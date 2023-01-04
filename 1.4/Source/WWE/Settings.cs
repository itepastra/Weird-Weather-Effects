using UnityEngine;
using Verse;

namespace WWE.Settings
{
    public class WWESettings : ModSettings
    {
        public bool razorGrassEnabled = true;
        public int razorGrassInterval = 200;
        public int razorGrassDamage = 1;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref razorGrassEnabled, "razorGrassEnabled");
            Scribe_Values.Look(ref razorGrassInterval, "razorGrassInterval", 200);
            Scribe_Values.Look(ref razorGrassDamage, "razorGrassDamage", 1);
            base.ExposeData();
        }
    }

    public class WWEMod : Mod
    {
        WWESettings settings;

        public WWEMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<WWESettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Weird Weather Events";
        }
    }
}
