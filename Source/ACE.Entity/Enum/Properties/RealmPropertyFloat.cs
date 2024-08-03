using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using RealmPropertyFloatAttribute = ACE.Entity.Enum.Properties.RealmPropertyPrimaryMinMaxAttribute<double>;

namespace ACE.Entity.Enum.Properties
{
#pragma warning disable IDE0001
    [RequiresPrimaryAttribute<RealmPropertyPrimaryMinMaxAttribute<double>, double>]
#pragma warning restore IDE0001
    public enum RealmPropertyFloat : ushort
    {
        [RealmPropertyFloat(defaultValue: 0f, minValue: 0f, maxValue: 0f)]
        Undef = 0,

        [Description("If you wish for players to glitch around less during powerslides, lower this value")]
        [RealmPropertyFloat(1f, 0.1f, 5f)]
        SpellCasting_MoveToState_UpdatePosition_Threshold = 1,

        [Description("The maximum angle a player allowed to face away from the target before releasing a spell. Defaults to server property 'spellcast_max_angle' if not defined in a ruleset")]
        [RealmPropertyFloat(defaultFromServerProperty: "spellcast_max_angle", 20f, 0f, 360f)]
        Spellcasting_Max_Angle = 2,

        [Description("The maximum distance a player may move during a spellcast without the cast being cancelled with \"Your movement disrupted spell casting!\"")]
        [RealmPropertyFloat(6f, 1f, 1000f)]
        SpellCastingPvPWindupMaxMove = 3,

        [Description("Creature health amounts will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.001f, 100f)]
        CreatureSpawnHPMultiplier = 4,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureSpawnRateMultiplier = 5,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureDamageMultiplier = 6,

        [Description("Creature strength attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureStrengthMultiplier = 7,

        [Description("Creature endurance attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureEnduranceMultiplier = 8,

        [Description("Creature coordination attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureCoordinationMultiplier = 9,

        [Description("Creature quickness attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureQuicknessMultiplier = 10,

        [Description("Creature focus attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureFocusMultiplier = 11,

        [Description("Creature self attribute will be multiplied by this amount")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureSelfMultiplier = 12,

        /*Below is not implemented*/
        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        PlayerDamageGlobalMultiplier = 13,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        PlayerDamageMeleeMultiplier = 14,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        PlayerDamageMissileMultiplier = 15,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        PlayerDamageMagicMultiplier = 16,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        PlayerChanceToEvadeMeleeCap = 17,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        PlayerChanceToDodgeMissileCap = 18,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        PlayerChanceToResistMagicCap = 19,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        CreatureChanceToEvadeMeleeCap = 20,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        CreatureChanceToDodgeMissileCap = 21,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0f, 1f)]
        CreatureChanceToResistMagicCap = 22,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureDamageGlobalMultiplier = 23,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureDamageMeleeMultiplier = 24,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        CreatureDamageMagicMultiplier = 25,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        PlayerCriticalDamageMultiplier = 26,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        HealingKitHealMultiplier = 27,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.01f, 100f)]
        FoodRestoreAmountMultiplier = 28,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.001f, 1000f)]
        ExperienceMultiplierAll = 29,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.001f, 1000f)]
        ExperienceMultiplierQuest = 30,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(1f, 0.001f, 1000f)]
        ExperienceMultiplierKills = 31,

        [Description("NOT IMPLEMENTED")]
        [RealmPropertyFloat(33f, -10f, 100f)]
        TinkeringBaseImbueChanceCap = 32,

        [Description("Scales the chance for cantrips to drop in each tier. Defaults to 1.0, as per end of retail")]
        [RerollRestrictedTo(RealmPropertyRerollType.landblock)]
        [RealmPropertyFloat("cantrip_drop_rate", 1f, 0f, 100000.0)]
        CantripDropRate = 33,

        [Description("Scales the chance for minor cantrips to drop, relative to other cantrip levels in the tier. Defaults to 1.0, as per end of retail")]
        [RerollRestrictedTo(RealmPropertyRerollType.landblock)]
        [RealmPropertyFloat("minor_cantrip_drop_rate", 1f, 0f, 100000.0)]
        MinorCantripDropRate = 34,

        [Description("Scales the chance for major cantrips to drop, relative to other cantrip levels in the tier. Defaults to 1.0, as per end of retail")]
        [RerollRestrictedTo(RealmPropertyRerollType.landblock)]
        [RealmPropertyFloat("major_cantrip_drop_rate", 1f, 0f, 100000.0)]
        MajorCantripDropRate = 35,

        [Description("Scales the chance for epic cantrips to drop, relative to other cantrip levels in the tier. Defaults to 1.0, as per end of retail")]
        [RerollRestrictedTo(RealmPropertyRerollType.landblock)]
        [RealmPropertyFloat("epic_cantrip_drop_rate", 1f, 0f, 100000.0)]
        EpicCantripDropRate = 36,

        [Description("Scales the chance for legendary cantrips to drop, relative to other cantrip levels in the tier. Defaults to 1.0, as per end of retail")]
        [RerollRestrictedTo(RealmPropertyRerollType.landblock)]
        [RealmPropertyFloat("legendary_cantrip_drop_rate", 1f, 0f, 100000.0)]
        LegendaryCantripDropRate = 37,

        // Pourtide props
        [RealmPropertyFloat("creature_spawn_multiplier", 1f, 0.01f, 100f)]
        CreatureSpawnMultiplier = 60001,

        [RealmPropertyFloat("creature_respawn_duration", 0f, 0f, 300f)]
        CreatureRespawnDuration = 60002,

        [Description("Scales the chance for a spell to chain to nearby monsters. Defaults to 0.0")]
        [RealmPropertyFloat("spell_chain_chance", 0f, 0f, 300f)]
        SpellChainChance = 60003,

        [Description("Sets the max range of a spell chain target. Defaults to 20.0")]
        [RealmPropertyFloat("max_spell_chain_range", 20f, 0f, 300f)]
        MaxSpellChainRange = 60004,

        [Description("Slayer Extractor Gem Drop Rate")]
        [RealmPropertyFloat("slayer_extractor_gem_drop_rate", 0f, 0f, 1f)]
        SlayerExtractorGemDropRate = 60005,

        [Description("Cantrip Extractor Gem Drop Rate")]
        [RealmPropertyFloat("cantrip_extractor_gem_drop_rate", 0f, 0f, 1f)]
        CantripExtractorGemDropRate = 60006,

        [Description("Major Upgrade Gem Drop Rate")]
        [RealmPropertyFloat("major_upgrade_gem_drop_rate", 0f, 0f, 1f)]
        MajorUpgradeGemDropRate = 60007,

        [Description("Epic Upgrade Gem Drop Rate")]
        [RealmPropertyFloat("epic_upgrade_gem_drop_rate", 0f, 0f, 1f)]
        EpicUpgradeGemDropRate = 60008,

        [Description("Legendary Upgrade Gem Drop Rate")]
        [RealmPropertyFloat("legendary_upgrade_gem_drop_rate", 0f, 0f, 1f)]
        LegendaryUpgradeGemDropRate = 60009,

        [Description("Spell Chain Morph Gem Drop Rate")]
        [RealmPropertyFloat("spell_chain_morph_gem_drop_rate", 0f, 0f, 1f)]
        SpellChainMorphGemDropRate = 60010,

        [Description("Thorn Armor Morph Gem Drop Rate")]
        [RealmPropertyFloat("thorn_armor_morph_gem_drop_rate", 0f, 0f, 1f)]
        ThornArmorMorphGemDropRate = 60011,

        [Description("Slow Weapon Morph Gem Drop Rate")]
        [RealmPropertyFloat("slow_weapon_morph_gem_drop_rate", 0f, 0f, 1f)]
        SlowWeaponMorphGemDropRate = 60012,

        [Description("Root Weapon Morph Gem Drop Rate")]
        [RealmPropertyFloat("root_weapon_morph_gem_drop_rate", 0f, 0f, 1f)]
        RootWeaponMorphGemDropRate = 60013,

        [Description("Forgotten Ore Drop Rate")]
        [RealmPropertyFloat("forgotten_ore_drop_rate", 0f, 0f, 1f)]
        ForgottenOreDropRate = 60014
    }

    public static class RealmPropertyFloatExtensions
    {
        public static string GetDescription(this RealmPropertyFloat prop)
        {
            var description = prop.GetAttributeOfType<DescriptionAttribute>();
            return description?.Description ?? prop.ToString();
        }
    }
}
