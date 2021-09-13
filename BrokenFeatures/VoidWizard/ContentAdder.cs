using BrokenFeatures;
using BrokenFeatures.VoidWizard;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;

namespace BrokenFeatures.NewContent
{
    class ContentAdder
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                Main.LogHeader("Loading New Content");
                AuraOfPrescienceFeature.AddAuraOfPrescienceFeature();
                RevealWeaknessFeature.AddRevealWeaknessFeature();
                VoidAwarenessFeature.AddVoidAwarenessFeature();
                VoidSchoolSpellList.AddVoidSchoolSpellList();
                VoidSchool.AddVoidSchool();
            }
        }
    }
}
