using BrokenFeatures.Utilities;
using Kingmaker.Blueprints.Classes.Spells;

namespace BrokenFeatures.VoidWizard
{
    class VoidSchoolSpellList
    {
        public static void AddVoidSchoolSpellList()
        {
            var VoidSchoolSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("VoidSchoolSpellList", bp =>
            {
                bp.SpellsByLevel = new SpellLevelList[]
                {
                    // Guidance
                    Helpers.CreateSpellLevelList(0, new string[] {
                        "c3a8f31778c3980498d8f00c980be5f5",
                    }),
                    // HurricaneBow, StunningBarrier, MageShield, TrueStrike
                    Helpers.CreateSpellLevelList(1, new string[] {
                        "3e9d1119d43d07c4c8ba9ebfd1671952",
                        "a5ec7892fb1c2f74598b3a82f3fd679f",
                        "ef768022b0785eb43a18969903c537c4",
                        "2c38da66e5a599347ac95b3294acbe00"
                    }),
                    // Invisibility, SeeInvisibility, SenseVitals, ResistEnergy, Blur
                    Helpers.CreateSpellLevelList(2, new string[] {
                        "89940cde01689fb46946b2f8cd7b66b7",
                        "30e5dc243f937fc4b95d2f8f4e1b7ff3",
                        "82962a820ebc0e7408b8582fdc3f4c0c",
                        "21ffef7791ce73f468b6fca4d9371e8b",
                        "14ec7a4e52e90fa47a4c8d63c69fd5c1"
                    }),
                    // DispelMagic, SeeInvisibilityCommunal, Displacement, ProtectionFromEnergy
                    Helpers.CreateSpellLevelList(3, new string[] {
                        "92681f181b507b34ea87018e8f7a528a",
                        "1a045f845778dc54db1c2be33a8c3c0a",
                        "903092f6488f9ce45a80943923576ab3",
                        "d2f116cfe05fcdd4a94e80143b67046f"
                    }),
                    // ProtectionFromEnergyCommunal, RemoveCurse, InvisibilityGreater
                    Helpers.CreateSpellLevelList(4, new string[] {
                        "76a629d019275b94184a1a8733cac45e",
                        "b48674cef2bff5e478a007cf57d8345b",
                        "ecaa0def35b38f949bd1976a6c9539e0"
                    }),
                    // MindFog, Feeblemind, Thoughtsense, BreakEnchantment
                    Helpers.CreateSpellLevelList(5, new string[] {
                        "eabf94e4edc6e714cabd96aa69f8b207",
                        "444eed6e26f773a40ab6e4d160c67faa",
                        "8fb1a1670b6e1f84b89ea846f589b627",
                        "7792da00c85b9e042a0fdfc2b66ec9a8"
                    }),
                    // DispelMagicGreater, TrueSeeingCast, Disintegrate
                    Helpers.CreateSpellLevelList(6, new string[] {
                        "f0f761b808dc4b149b08eaf44b99f633",
                        "4cf3d0fae3239ec478f51e86f49161cb",
                        "4aa7942c3e62a164387a73184bca3fc1"
                    }),
                    // TrueSeeingCommunal, InvisibilityMass, WalkThroughSpace
                    Helpers.CreateSpellLevelList(7, new string[] {
                        "fa08cb49ade3eee42b5fd42bd33cb407",
                        "98310a099009bbd4dbdf66bcef58b4cd",
                        "368d7cf2fb69d8a46be5a650f5a5a173"
                    }),
                    // RiftOfRuin, MindBlank, ProtectionFromSpells
                    Helpers.CreateSpellLevelList(8, new string[] {
                        "dd3dacafcf40a0145a5824c838e2698d",
                        "df2a0ba6b6dcecf429cbb80a56fee5cf",
                        "42aa71adc7343714fa92e471baa98d42"
                    }),
                    // Foresight, MindBlankCommunal, EnergyDrain
                    Helpers.CreateSpellLevelList(9, new string[] {
                        "1f01a098d737ec6419aedc4e7ad61fdd",
                        "87a29febd010993419f2a4a9bee11cfc",
                        "37302f72b06ced1408bf5bb965766d46"
                    })
                };
            });
        }
    }
}
