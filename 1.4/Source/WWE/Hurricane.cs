using RimWorld;
using System.Collections.Generic;
using Verse;

namespace WWE.RegularEvents
{
    internal class Hurricane : GameCondition
    {
        Settings.WWESettings Settings;

        int RoofCollapseDelay = 100;
        int RoofCollapsePerCycle = 5;

        private readonly IntVec3 RandomDefault = new IntVec3(-1000, -1000, -1000);

        public Hurricane() : base()
        {
            this.Settings = LoadedModManager.GetMod<Settings.WWEMod>().GetSettings<Settings.WWESettings>();
        }
        public override void GameConditionTick()
        {
            List<Map> affectedMaps = base.AffectedMaps;
            foreach (Map map in affectedMaps)
            {
                if (Find.TickManager.TicksGame % RoofCollapseDelay == 0)
                {
                    RemoveRoofs(map);
                }
            }
        }

        private void RemoveRoofs(Map map)
        {
            for (int i = 0; i < RoofCollapsePerCycle; i++)
            {
                IntVec3 cell = CellFinderLoose.RandomCellWith((IntVec3 v) => { RoofDef r = map.roofGrid.RoofAt(v); return r != null ? !r.isNatural : false; }, map);
                if (cell != RandomDefault)
                {
                    Log.Warning("changing roof at cell " + cell.ToString());
                    map.roofGrid.SetRoof(cell, null);
                    Log.Warning("changed roof");
                }
            }
        }

    }
}
