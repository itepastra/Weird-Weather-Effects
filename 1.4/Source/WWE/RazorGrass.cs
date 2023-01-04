using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace WWE.RegularEvents
{
    public class RazorGrass : GameCondition
    {
        float RazorGrassDamage = 1;
        int RazorGrassInterval = 200;

        public RazorGrass() : base()
        {
        }

        public override void GameConditionTick()
        {
            List<Map> affectedMaps = base.AffectedMaps;
            foreach (Map map in affectedMaps)
            {
                if (Find.TickManager.TicksGame % RazorGrassInterval == 0)
                {
                    DoPawnGrassDamage(map);
                }
            }
        }

        private void DoPawnGrassDamage(Map map)
        {
            List<Pawn> allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
            for (int i = 0; i < allPawnsSpawned.Count; i++)
            {
                Pawn pawn = allPawnsSpawned[i];
                Thing dangerGrass = map.thingGrid.ThingAt(pawn.Position, ThingDefOf.Plant_Grass);
                if (dangerGrass != null)
                {
                    DoPawnDamage(pawn, dangerGrass);
                }
            }
        }

        private void DoPawnDamage(Pawn p, Thing grass)
        {
            if (p.Downed)
            {
                return;
            }
            HediffSet hediffSet = p.health.hediffSet;
            IEnumerable<BodyPartRecord> source = from x in HittablePartsViolence(hediffSet)
                                                 where !p.health.hediffSet.hediffs.Any((Hediff y) => y.Part == x && y.CurStage != null && y.CurStage.partEfficiencyOffset < 0f)
                                                 select x;
            BodyPartRecord bodyPartRecord = source.RandomElementByWeight((BodyPartRecord x) => x.coverage);
            DamageDef damageDef = DamageDefOf.Cut;
            HediffDef hediffDefFromDamage = HealthUtility.GetHediffDefFromDamage(damageDef, p, bodyPartRecord);
            DamageInfo dinfo = new DamageInfo(damageDef, RazorGrassDamage, 2, -1f, grass, bodyPartRecord);
            dinfo.SetAllowDamagePropagation(val: false);
            p.TakeDamage(dinfo);
        }

        private static IEnumerable<BodyPartRecord> HittablePartsViolence(HediffSet bodyModel)
        {
            return from x in bodyModel.GetNotMissingParts()
                   where x.depth == BodyPartDepth.Outside
                   select x;
        }
    }
}
