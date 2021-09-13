using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using BrokenFeatures.Extensions;
using BrokenFeatures.Utilities;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics;

namespace BrokenFeatures.VoidWizard
{
    class VoidAwarenessFeature
    {
        public static void AddVoidAwarenessFeature()
        {

            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var WizardClassReferenceArray = new BlueprintCharacterClassReference[] { WizardClass.ToReference<BlueprintCharacterClassReference>() };

            var VoidAwarenessIcon = AssetLoader.LoadInternal("Abilities", "Icon_AuraOfPrescience.png");

            var VoidAwarenessFeature = Helpers.CreateBlueprint<BlueprintFeature>("VoidAwarenessFeature", bp =>
            {
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.SetName("Void Awareness");
                bp.SetDescription("Your ability to recognize the void allows your body to react to magical manifestations before you’re even aware of them. You gain a +2 insight bonus on saving throws against spells and spell-like abilities. This bonus increases by +1 for every five wizard levels you possess. "
                    + "At 20th level, whenever you would be affected by a spell or spell - like ability that allows a saving throw, you can roll twice to save against the effect and take the better result.");
                bp.m_Icon = VoidAwarenessIcon;

                bp.AddComponent(Helpers.Create<ContextRankConfig>(c =>
                {
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_Min = 2;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 5;
                    c.m_UseMax = false;
                    c.m_UseMin = true;
                    c.m_Class = WizardClassReferenceArray;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                }));

                bp.AddComponent(Helpers.Create<SavingThrowBonusAgainstAbilityType>(b =>
                {
                    b.ModifierDescriptor = ModifierDescriptor.Insight;
                    b.Value = 0;
                    b.Bonus = Helpers.Create<ContextValue>(v =>
                    {
                        v.ValueType = ContextValueType.Rank;
                    });
                }));

            });


        }
    }
}
