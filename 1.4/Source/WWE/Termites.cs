using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace WWE.InstantEvents
{
    public class Termites : IncidentWorker
    {

        private float DamagePercentage = 0.9f;
        private List<ThingDef> edible = new List<ThingDef> { ThingDefOf.Plant_TreeOak,
            ThingDefOf.Plant_TreePolux,
            ThingDefOf.Plant_TreeGauranlen,
            ThingDefOf.Plant_TreeAnima,
            ThingDef.Named("Plant_TreePine"),
            ThingDef.Named("Plant_TreeBirch"),
            ThingDef.Named("Plant_TreePoplar") };
        private List<ThingDef> edibleMaterials = new List<ThingDef> { ThingDefOf.WoodLog };

        private void TermiteDamageTile(IEnumerable<Thing> things, float damagePercentage)
        {
            var things2 = things.ToList(); // otherwise it modifies the original enumerable and breaks things
            foreach (Thing item in things2)
            {
                if (edible.Contains(item.def) || edibleMaterials.Contains(item.def) || edibleMaterials.Contains(item.Stuff))
                {
                    float amount = (float)item.MaxHitPoints * damagePercentage;
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.Deterioration, amount);
                    item.TakeDamage(dinfo);
                }
            }
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            int cellIndices = map.cellIndices.NumGridCells;
            ThingGrid things = map.thingGrid;
            for (int i = 0; i < cellIndices; i++)
            {
                List<Thing> things1 = things.ThingsListAtFast(i);
                TermiteDamageTile(things1, DamagePercentage);
            }
            return true;
        }
    }
}
