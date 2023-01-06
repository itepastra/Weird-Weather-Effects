using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace WWE.HediffGivers
{
    public class Mountain_Poisoning : HediffGiver
    {
        private const int PoisonCheckInterval = 420;

        public static readonly string MemoPawnBurnedByAir = "Pawn sufficated in toxin";


        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            if (pawn.IsWorldPawn()) { return; }

            if (!pawn.Map.GameConditionManager.ConditionIsActive(GameConditionDef.Named("WWE_Poison_Mountains")))
            {
                return;
            }
            IntVec3 pos = pawn.Position;

            Hediff h = pawn.health.hediffSet.GetFirstHediffOfDef(hediff);
            RoofDef rooftype = pos.GetRoof(pawn.Map);
            if (rooftype == RoofDefOf.RoofRockThick)
            {
                // add severity if in the mountains
                HealthUtility.AdjustSeverity(pawn, hediff, 0.0005f);
            }
            else if (h != null && rooftype == RoofDefOf.RoofConstructed && h.Severity > 0.5f)
            {
                h.Severity = Mathf.Clamp(h.Severity-0.0001f, 0.5f, 1.0f);
            }
            else if (h != null && rooftype == null)
            {
                h.Severity -= 0.0001f;
            }

            if (pawn.Dead || !pawn.IsNestedHashIntervalTick(60, 420)) { return; }

        }
    }
}
