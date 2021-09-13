using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using BrokenFeatures.Extensions;
using BrokenFeatures.Utilities;
using Kingmaker.Blueprints.Classes.Selection;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;

namespace BrokenFeatures.VoidWizard
{
    class VoidSchool
    {
        public static void AddVoidSchool()
        {
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var WizardClassReferenceArray = new BlueprintCharacterClassReference[] { WizardClass.ToReference<BlueprintCharacterClassReference>() };
            var WizardSchools = Resources.GetBlueprint<BlueprintFeatureSelection>("8d4637639441f1041bee496f20af7fa3");
            var SpecialistSchoolSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5f838049069f1ac4d804ce0862ab5110");

            var VoidSchoolIcon = AssetLoader.LoadInternal("Abilities", "Icon_AuraOfPrescience.png");


            // Reference variables for later
            var AuraOfPrescience = Resources.GetBlueprint<BlueprintFeature>("2ded002a-790f-4727-8b20-ad73b9c0a670");
            var AuraOfPrescienceResource = Resources.GetBlueprint<BlueprintAbilityResource>("2ded002a-790f-4727-8b20-ad73b9c0a666");
            var AuraOfPrescienceFeatureUnitFactReference = AuraOfPrescience.ToReference<BlueprintUnitFactReference>();

            var RevealWeakness = Resources.GetBlueprint<BlueprintFeature>("2ded002a-790f-4727-8b20-ad73b9c0a676");
            var RevealWeaknessResource = Resources.GetBlueprint<BlueprintAbilityResource>("2ded002a-790f-4727-8b20-ad73b9c0a677");
            var RevealWeaknessFeatureUnitFactReference = RevealWeakness.ToReference<BlueprintUnitFactReference>();

            var VoidAwareness = Resources.GetBlueprint<BlueprintFeature>("2ded002a-790f-4727-8b20-ad73b9c0a678");
            var VoidAwarenessFeatureUnitFactReference = VoidAwareness.ToReference<BlueprintUnitFactReference>();

            var VoidSpellList = Resources.GetBlueprint<BlueprintFeature>("2ded002a-790f-4727-8b20-ad73b9c0a679");
            var VoidSpellListFeatureUnitFactReference = VoidSpellList.ToReference<BlueprintUnitFactReference>();



            var VoidSchoolBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("VoidSchoolFeature", bp =>
            {
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.SetDescription("The void is a complex idea considered by some to be a fifth element—one that comprises thought, space, spiritualism, and insight. Wizards who tap into its mysterious powers gain control over energies that bind the earth to the heavens and the planes to their mysterious and eternal alignments, as well as the powers that stitch living beings to their spirits.");
                bp.SetName("Specialist School - Void");
                bp.m_Icon = VoidSchoolIcon;

                bp.AddComponent(Helpers.Create<AddFacts>(f =>
                {
                    f.m_Facts = new BlueprintUnitFactReference[]
                    {
                        RevealWeaknessFeatureUnitFactReference,
                        VoidAwarenessFeatureUnitFactReference
                    };
                }));

                bp.AddComponent(Helpers.Create<AddAbilityResources>(f =>
                {
                    f.RestoreAmount = true;
                    f.m_Resource = RevealWeaknessResource.ToReference<BlueprintAbilityResourceReference>();
                }));


                bp.AddComponent(Helpers.Create<AddSpecialSpellList>(sl =>
                {
                    sl.m_CharacterClass = WizardClassReferenceArray[0];
                    sl.m_SpellList = Resources.GetBlueprint<BlueprintSpellList>("2ded002a-790f-4727-8b20-ad73b9c0a679").ToReference<BlueprintSpellListReference>();
                }));

            });

            var VoidSchoolGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("VoidSchoolGreaterFeature", bp =>
            {
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.SetName("Aura of Prescience");
                bp.m_Icon = VoidSchoolIcon;

                bp.AddComponent(Helpers.Create<AddFacts>(f =>
                {
                    f.m_Facts = new BlueprintUnitFactReference[]
                    {
                        AuraOfPrescienceFeatureUnitFactReference
                    };
                }));

                bp.AddComponent(Helpers.Create<AddAbilityResources>(f =>
                {
                    f.RestoreAmount = true;
                    f.m_Resource = AuraOfPrescienceResource.ToReference<BlueprintAbilityResourceReference>();
                }));

            });

            var SpecialisationSchoolVoidProgression = Helpers.CreateBlueprint<BlueprintProgression>("SpecialisationSchoolVoidProgression", bp =>
            {
                bp.Groups = new FeatureGroup[] { FeatureGroup.SpecialistSchool };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = VoidSchoolIcon;
                bp.Comment = "";
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[0];
                bp.m_AlternateProgressionClasses = new BlueprintProgression.ClassWithLevel[0];
                bp.SetDescription("The void is a complex idea considered by some to be a fifth element—one that comprises thought, space, spiritualism, and insight. Wizards who tap into its mysterious powers gain control over energies that bind the earth to the heavens and the planes to their mysterious and eternal alignments, as well as the powers that stitch living beings to their spirits.");
                bp.SetName("Specialist School - Void");

                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WizardClass.ToReference<BlueprintCharacterClassReference>()
                    } };

                bp.LevelEntries = new LevelEntry[]
                {
                    Helpers.Create<LevelEntry>(e =>
                    {
                        e.m_Features = new List<BlueprintFeatureBaseReference>
                        {
                            VoidSchoolBaseFeature.ToReference<BlueprintFeatureBaseReference>()
                        };
                        e.Level = 1;
                    }),
                    Helpers.Create<LevelEntry>(e =>
                    {
                        e.m_Features = new List<BlueprintFeatureBaseReference>
                        {
                            VoidSchoolGreaterFeature.ToReference<BlueprintFeatureBaseReference>()
                        };
                        e.Level = 8;
                    }),
                };
                bp.m_UIDeterminatorsGroup = Resources.GetBlueprint<BlueprintProgression>("567801abe990faf4080df566fadcd038").m_UIDeterminatorsGroup;

            });

            // Actually add the school feature to the selectable list
            WizardSchools.m_AllFeatures.AppendToArray(VoidSchoolBaseFeature.ToReference<BlueprintFeatureReference>());
            SpecialistSchoolSelection.m_AllFeatures = SpecialistSchoolSelection.m_AllFeatures.AppendToArray(SpecialisationSchoolVoidProgression.ToReference<BlueprintFeatureReference>());
        }
    }
}
