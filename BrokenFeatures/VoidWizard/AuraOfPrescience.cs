using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using BrokenFeatures.Extensions;
using BrokenFeatures.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Buffs;

namespace BrokenFeatures.VoidWizard
{
    class AuraOfPrescienceFeature
    {
        public static void AddAuraOfPrescienceFeature()
        {
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var WizardClassReferenceArray = new BlueprintCharacterClassReference[] { WizardClass.ToReference<BlueprintCharacterClassReference>() };



            // The resource to be consumed by the wizard when using Aura of Prescience
            var AuraOfPrescienceResourceAmount = new BlueprintAbilityResource.Amount
            {
                BaseValue = 1,
                IncreasedByLevel = true,
                m_Class = WizardClassReferenceArray,
                LevelIncrease = 1,
                m_ArchetypesDiv = new BlueprintArchetypeReference[0],
            };
            var AuraOfPrescienceResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("AuraOfPrescienceResource", bp =>
            {
                bp.m_UseMax = true;
                bp.m_Max = 20;
                bp.m_MaxAmount = AuraOfPrescienceResourceAmount;
            });

            var AuraOfPrescienceBuff = Helpers.CreateBuff("AuraOfPrescienceBuff", bp =>
            {
                bp.SetName("Aura of Prescience");
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.m_Icon = AssetLoader.LoadInternal("Abilities", "Icon_AuraOfPrescience.png");
                bp.IsClassFeature = true;
                bp.AddComponent(Helpers.Create<AbilityScoreCheckBonus>(bonus =>
                {
                    bonus.Bonus = 2;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(bonus =>
                {
                    bonus.Stat = StatType.AdditionalAttackBonus;
                    bonus.Value = 2;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(bonus =>
                {
                    bonus.Stat = StatType.AdditionalDamage;
                    bonus.Value = 2;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
                bp.AddComponent(Helpers.Create<BuffAllSavesBonus>(bonus =>
                {
                    bonus.Value = 2;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
                bp.AddComponent(Helpers.Create<BuffAllSkillsBonus>(bonus =>
                {
                    bonus.Value = 2;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
            });

        }
    }
}
