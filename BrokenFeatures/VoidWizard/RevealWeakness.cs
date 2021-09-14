using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using BrokenFeatures.Extensions;
using BrokenFeatures.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Craft;

namespace BrokenFeatures.VoidWizard
{
    class RevealWeaknessFeature
    {
        public static void AddRevealWeaknessFeature()
        {
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var WizardClassReferenceArray = new BlueprintCharacterClassReference[] { WizardClass.ToReference<BlueprintCharacterClassReference>() };

            var RevealWeaknessIcon = AssetLoader.LoadInternal("Abilities", "Icon_RevealWeakness.png");

            // The resource to be consumed by the wizard when using Aura of Prescience
            var RevealWeaknessResourceAmount = new BlueprintAbilityResource.Amount
            {
                BaseValue = 3,
                IncreasedByStat = true,
                ResourceBonusStat = StatType.Intelligence,
                m_Class = WizardClassReferenceArray,
            };

            var RevealWeaknessResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("RevealWeaknessResource", bp =>
            {
                bp.m_MaxAmount = RevealWeaknessResourceAmount;
                bp.m_Icon = RevealWeaknessIcon;
            });

            var RevealWeaknessDebuff = Helpers.CreateBuff("RevealWeaknessDebuff", bp =>
            {
                bp.SetName("Reveal Weakness");
                bp.SetDescription("When you activate this school power as a standard action, you select a foe within 30 feet. That creature takes a penalty to its AC and on saving throws equal to 1/2 your caster level (minimum –1) for 1 round. You can use this ability a number of times per day equal to 3 + your Intelligence bonus.");
                bp.m_Icon = RevealWeaknessIcon;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;

                bp.AddComponent(Helpers.Create<ContextRankConfig>(c =>
                {
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Min = 1;
                    c.m_UseMax = false;
                    c.m_UseMin = true;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(b =>
                {
                    b.Stat = StatType.SaveWill;
                    b.Multiplier = -1;
                    b.Descriptor = ModifierDescriptor.UntypedStackable;
                    b.Value = Helpers.Create<ContextValue>(v =>
                    {
                        v.ValueType = ContextValueType.Rank;
                    });
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(b =>
                {
                    b.Stat = StatType.SaveFortitude;
                    b.Descriptor = ModifierDescriptor.UntypedStackable;
                    b.Multiplier = -1;
                    b.Value = Helpers.Create<ContextValue>(v =>
                    {
                        v.ValueType = ContextValueType.Rank;
                    });
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(b =>
                {
                    b.Stat = StatType.SaveReflex;
                    b.Descriptor = ModifierDescriptor.UntypedStackable;
                    b.Multiplier = -1;
                    b.Value = Helpers.Create<ContextValue>(v =>
                    {
                        v.ValueType = ContextValueType.Rank;
                    });
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(b =>
                {
                    b.Stat = StatType.AC;
                    b.Descriptor = ModifierDescriptor.UntypedStackable;
                    b.Multiplier = -1;
                    b.Value = Helpers.Create<ContextValue>(v =>
                    {
                        v.ValueType = ContextValueType.Rank;
                    });
                }));
            });

            var RevealWeaknessAbility = Helpers.CreateBlueprint<BlueprintAbility>("RevealWeaknessAbility", bp =>
            {
                bp.SetName("Reveal Weakness");
                bp.SetDescription("When you activate this school power as a standard action, you select a foe within 30 feet. That creature takes a penalty to its AC and on saving throws equal to 1/2 your caster level (minimum –1) for 1 round. You can use this ability a number of times per day equal to 3 + your Intelligence bonus.");
                bp.m_Icon = RevealWeaknessIcon;
                bp.CanTargetPoint = false;
                bp.CanTargetFriends = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetSelf = true;
                bp.LocalizedDuration = Helpers.CreateString("1_round_duration", "1 Round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.ActionType = CommandType.Standard;

                bp.AddComponent(Helpers.Create<AbilityResourceLogic>(a =>
                {
                    a.Amount = 1;
                    a.m_IsSpendResource = true;
                    a.m_RequiredResource = RevealWeaknessResource.ToReference<BlueprintAbilityResourceReference>();
                }));

                bp.AddComponent(Helpers.Create<CraftInfoComponent>(c =>
                {
                    c.SpellType = CraftSpellType.Debuff;
                    c.AOEType = CraftAOE.None;
                    c.SavingThrow = CraftSavingThrow.None;
                }));

                bp.AddComponent(Helpers.Create<AbilityEffectRunAction>(a =>
                {
                    a.SavingThrowType = 0;
                    a.AddAction(Helpers.Create<ContextActionApplyBuff>(b =>
                    {
                        b.m_Buff = RevealWeaknessDebuff.ToReference<BlueprintBuffReference>();
                        b.UseDurationSeconds = true;
                        b.DurationSeconds = 6.0F;
                    }));
                }));
            });

            var RevealWeaknessFeature = Helpers.CreateBlueprint<BlueprintFeature>("RevealWeaknessFeature", bp =>
            {
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.SetName("Reveal Weakness");
                bp.SetDescription("When you activate this school power as a standard action, you select a foe within 30 feet. That creature takes a penalty to its AC and on saving throws equal to 1/2 your caster level (minimum –1) for 1 round. You can use this ability a number of times per day equal to 3 + your Intelligence bonus.");
                bp.m_Icon = RevealWeaknessIcon;

                bp.AddComponent(Helpers.Create<AddAbilityResources>(f =>
                {
                    f.m_Resource = RevealWeaknessResource.ToReference<BlueprintAbilityResourceReference>();
                }));

                bp.AddComponent(Helpers.Create<AddFacts>(f =>
                {
                    f.m_Facts = new BlueprintUnitFactReference[] { RevealWeaknessAbility.ToReference<BlueprintUnitFactReference>() };
                }));
            });


        }
    }
}
