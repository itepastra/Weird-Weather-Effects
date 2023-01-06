using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace WWE.HediffGivers
{
    public class Transitioning : HediffGiver
    {
        private const int TransitionCheckInterval = 420;

        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            if (pawn.IsWorldPawn()) { return; }
            if (!pawn.Map.GameConditionManager.ConditionIsActive(GameConditionDef.Named("WWE_Genderfluid_Rain")))
            {
                return;
            }

            IntVec3 pos = pawn.Position;
            Hediff h = pawn.health.hediffSet.GetFirstHediffOfDef(hediff);
            if (pawn.Map.roofGrid.Roofed(pos))
            {
                if (h != null)
                {
                    h.Severity -= 0.001f;
                }
            }
            else
            {
                HealthUtility.AdjustSeverity(pawn, hediff, 0.005f);
            }
            if (h != null && h.Severity >= 1)
            {
                HealthUtility.Cure(h);

                switch (pawn.gender)
                {
                    case Gender.Male:
                        pawn.gender = Gender.Female;
                        break;
                    case Gender.Female:
                        pawn.gender = Gender.Male;
                        break;
                    default:
                        break;
                }

                Hediff h2 = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("WWE_Transitioned"));
                if (h2 != null)
                {
                    HealthUtility.Cure(h2);
                }
                else
                {
                    HealthUtility.AdjustSeverity(pawn, HediffDef.Named("WWE_Transitioned"), 1f);
                }
                return;
            }
        }
    }
}
