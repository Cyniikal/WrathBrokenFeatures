using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using BrokenFeatures.Extensions;
using BrokenFeatures.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.ActivatableAbilities;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace BrokenFeatures.VoidWizard
{
    class AuraOfPrescienceFeature
    {
        public static void AddAuraOfPrescienceFeature()
        {
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var WizardClassReferenceArray = new BlueprintCharacterClassReference[] { WizardClass.ToReference<BlueprintCharacterClassReference>() };

            var AuraOfPrescienceIcon = AssetLoader.LoadInternal("Abilities", "Icon_AuraOfPrescience.png");

            // The resource to be consumed by the wizard when using Aura of Prescience
            var AuraOfPrescienceResourceAmount = new BlueprintAbilityResource.Amount
            {
                BaseValue = 1,
                m_Class = WizardClassReferenceArray,
                IncreasedByLevel = true,
                LevelIncrease = 1,
                m_ArchetypesDiv = new BlueprintArchetypeReference[0],
            };

            var AuraOfPrescienceResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("AuraOfPrescienceResource", bp =>
            {
                bp.m_UseMax = true;
                bp.m_Max = 20;
                bp.m_MaxAmount = AuraOfPrescienceResourceAmount;
                bp.m_Icon = AuraOfPrescienceIcon;
            });

            var AuraOfPrescienceEffectBuff = Helpers.CreateBuff("AuraOfPrescienceEffectBuff", bp =>
            {
                bp.SetName("Aura of Prescience");
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.m_Icon = AuraOfPrescienceIcon;
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
                    bonus.Multiplier = 1;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(bonus =>
                {
                    bonus.Value = 2;
                    bonus.Stat = StatType.Initiative;
                    bonus.Descriptor = ModifierDescriptor.Insight;
                }));
            });

            var AuraOfPrescienceAOE = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfPrescienceAOE", area =>
            {
                area.AggroEnemies = false;
                area.Shape = AreaEffectShape.Cylinder;
                area.Size = new Kingmaker.Utility.Feet { m_Value = 30 };

                area.Fx = new Kingmaker.ResourceLinks.PrefabLink();

                area.AddComponent(Helpers.Create<AbilityAreaEffectBuff>(aoe =>
                {
                    aoe.m_Buff = AuraOfPrescienceEffectBuff.ToReference<BlueprintBuffReference>();

                    aoe.Condition = Helpers.Create<ConditionsChecker>(c =>
                    {
                        c.Conditions = new Condition[] { Helpers.Create<ContextConditionIsAlly>(a => { }) };
                    });
                }));
                area.Comment = "";

            });

            var AuraOfPrescienceBuff = Helpers.CreateBuff("AuraOfPrescienceBuff", bp =>
            {
                bp.SetName("Aura of Prescience");
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_Icon = AuraOfPrescienceIcon;
                bp.IsClassFeature = true;
                bp.AddComponent(Helpers.Create<AddAreaEffect>(aoe =>
                {
                    aoe.m_AreaEffect = AuraOfPrescienceAOE.ToReference<BlueprintAbilityAreaEffectReference>();
                }));
            });

            

            var AuraOfPrescienceActivatableAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("AuraOfPrescienceActivatableAbility", bp =>
            {
                bp.SetName("Aura of Prescience");
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.m_Icon = AuraOfPrescienceIcon;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = true;
                bp.WeightInGroup = 1;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = CommandType.Standard;
                bp.m_Buff = AuraOfPrescienceBuff.ToReference<BlueprintBuffReference>();

                bp.AddComponent(Helpers.Create<ActivatableAbilityResourceLogic>(a =>
                {
                    a.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    a.m_RequiredResource = AuraOfPrescienceResource.ToReference<BlueprintAbilityResourceReference>();
                }));

            });

            var AuraOfPrescienceFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfPrescienceFeature", bp =>
            {
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.SetDescription("This aura grants a +2 insight bonus on ability checks, attack rolls, damage rolls, saving throws, and skill checks to all allies with 30 feet of the Void Wizard.");
                bp.SetName("Aura of Prescience");
                bp.m_Icon = AuraOfPrescienceIcon;

                bp.AddComponent(Helpers.Create<AddAbilityResources>(f =>
                {
                    f.m_Resource = AuraOfPrescienceResource.ToReference<BlueprintAbilityResourceReference>();
                }));

                bp.AddComponent(Helpers.Create<AddFacts>(f =>
                {
                    f.m_Facts = new BlueprintUnitFactReference[] { AuraOfPrescienceActivatableAbility.ToReference<BlueprintUnitFactReference>() };
                }));
            });


        }
    }
}
