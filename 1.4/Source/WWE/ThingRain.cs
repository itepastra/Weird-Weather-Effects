using RimWorld;
using System.Collections.Generic;
using Verse;

namespace WWE.RegularEvents
{


    public class ThingDefRain : GameCondition
    {
        int RainInterval = 5;

        private readonly IntVec3 RandomDefault = new IntVec3(-1000, -1000, -1000);

        private ThingDef ThingToRain;
        private ThingDef StuffOfThing;

        public ThingDefRain(ThingDef def, int interval, ThingDef stuffOfThing=null)
        {
            this.ThingToRain = def;
            this.RainInterval = interval;
            StuffOfThing = stuffOfThing;
        }


        public override void GameConditionTick()
        {
            if (Find.TickManager.TicksGame % RainInterval == 0)
            {

                List<Map> affectedMaps = base.AffectedMaps;

                foreach (Map map in affectedMaps)
                {
                    MapTick(map);
                }
            }
        }

        private void MapTick(Map map)
        {
            // Select a random cell with a non-natural roof.
            IntVec3 cell = CellFinderLoose.RandomCellWith((IntVec3 v) => map.roofGrid.RoofAt(v) == null, map);
            if (cell != RandomDefault)
            {
                Thing thing = ThingMaker.MakeThing(ThingToRain, StuffOfThing);
                GenSpawn.Spawn(thing, cell, map);
            }

        }

        public override WeatherDef ForcedWeather()
        {
            return WeatherDefOf.Clear;
        }
    }
}