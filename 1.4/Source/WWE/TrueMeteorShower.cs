using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWE.RegularEvents
{
    internal class TrueMeteorShower : ThingDefRain
    {
        public TrueMeteorShower() : base(ThingDefOf.MineableComponentsIndustrial, 3) { }
    }
}
