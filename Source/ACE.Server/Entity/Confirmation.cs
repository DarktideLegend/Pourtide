using System;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Server.Managers;
using ACE.Server.WorldObjects;
using log4net.Core;

namespace ACE.Server.Entity
{
    public abstract class Confirmation
    {
        public ObjectGuid PlayerGuid;

        public ConfirmationType ConfirmationType;

        public uint ContextId;

        public Confirmation(ObjectGuid playerGuid, ConfirmationType confirmationType)
        {
            PlayerGuid = playerGuid;

            ConfirmationType = confirmationType;
        }

        public virtual void ProcessConfirmation(bool response, bool timeout = false)
        {
            // empty base
        }

        public Player Player => PlayerManager.GetOnlinePlayer(PlayerGuid);
    }

    public class Confirmation_AlterAttribute: Confirmation
    {
        public ObjectGuid AttributeTransferDevice;

        public Confirmation_AlterAttribute(ObjectGuid playerGuid, ObjectGuid attributeTransferDevice)
            : base(playerGuid, ConfirmationType.AlterAttribute)
        {
            AttributeTransferDevice = attributeTransferDevice;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            if (!response) return;

            var player = Player;
            if (player == null) return;

            var attributeTransferDevice = player.FindObject(AttributeTransferDevice, Player.SearchLocations.MyInventory) as AttributeTransferDevice;

            if (attributeTransferDevice != null)
                attributeTransferDevice.ActOnUse(player, true);
        }
    }

    public class Confirmation_AlterSkill : Confirmation
    {
        public ObjectGuid SkillAlterationDevice;

        public Confirmation_AlterSkill(ObjectGuid playerGuid, ObjectGuid skillAlterationDevice)
            : base(playerGuid, ConfirmationType.AlterSkill)
        {
            SkillAlterationDevice = skillAlterationDevice;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            if (!response) return;

            var player = Player;
            if (player == null) return;

            var skillAlterationDevice = player.FindObject(SkillAlterationDevice, Player.SearchLocations.MyInventory) as SkillAlterationDevice;

            if (skillAlterationDevice != null)
                skillAlterationDevice.ActOnUse(player, true);
        }
    }

    public class Confirmation_Augmentation: Confirmation
    {
        public ObjectGuid AugmentationGuid;

        public Confirmation_Augmentation(ObjectGuid playerGuid, ObjectGuid augmentationGuid)
            : base(playerGuid, ConfirmationType.Augmentation)
        {
            AugmentationGuid = augmentationGuid;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            if (!response) return;

            var player = Player;
            if (player == null) return;

            var augmentation = player.FindObject(AugmentationGuid, Player.SearchLocations.MyInventory) as AugmentationDevice;

            if (augmentation != null)
                augmentation.ActOnUse(player, true);
        }
    }

    public class Confirmation_CraftInteration: Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;

        public bool Tinkering;

        public Confirmation_CraftInteration(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid)
            : base (playerGuid, ConfirmationType.CraftInteraction)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.UseObjectOnTarget(player, source, target, true);
        }
    }

    public class Confirmation_ApplyCantripExtractor: Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;
        public int Id;

        public Confirmation_ApplyCantripExtractor(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid, int id)
            : base (playerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
            Id = id;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.HandleApplyCantripExtractorGemConfirmed(player, source, target, Id);
        }
    }

    public class Confirmation_ApplyCantripMorphGem: Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;
        public int Id;

        public Confirmation_ApplyCantripMorphGem(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid, int id)
            : base (playerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
            Id = id;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.HandleApplyCantripMorphGemConfirmed(player, source, target, Id);
        }
    }

    public class Confirmation_ApplySlayerExtractor: Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;
        public WorldObject Gem;

        public Confirmation_ApplySlayerExtractor(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid, WorldObject gem)
            : base (playerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
            Gem = gem;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.HandleApplySlayerExtractorConfirmed(player, source, target, Gem);
        }
    }

    public class Confirmation_ApplyCantripUpgrade: Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;
        public string Spell;
        public char Level;
        public int Value;
        public int Id;


        public Confirmation_ApplyCantripUpgrade(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid, string spell, char level, int value, int id)
            : base (playerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
            Spell = spell;
            Level = level;
            Value = value;
            Id = id;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.HandleApplyCantripUpgradeGemConfirmed(player, source, target, Spell, Level, Value, Id);
        }
    }

    public class Confirmation_ApplyThornArmor : Confirmation
    {
        public ObjectGuid SourceGuid;
        public ObjectGuid TargetGuid;
        public float ReflectiveDamageModifier;


        public Confirmation_ApplyThornArmor(ObjectGuid playerGuid, ObjectGuid sourceGuid, ObjectGuid targetGuid, float reflectiveDamageModifier)
            : base(playerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            TargetGuid = targetGuid;
            ReflectiveDamageModifier = reflectiveDamageModifier;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            if (!response)
            {
                player.SendWeenieError(WeenieError.YouChickenOut);

                return;
            }

            // inventory only?
            var source = player.FindObject(SourceGuid, Player.SearchLocations.LocationsICanMove);
            var target = player.FindObject(TargetGuid, Player.SearchLocations.LocationsICanMove);

            if (source == null || target == null) return;

            RecipeManager.HandleApplyThornArmorMorphGemConfirmed(player, source, target, ReflectiveDamageModifier);
        }
    }

    public class Confirmation_Fellowship : Confirmation
    {
        public ObjectGuid InviterGuid;

        public Confirmation_Fellowship(ObjectGuid inviterGuid, ObjectGuid invitedGuid)
            : base(invitedGuid, ConfirmationType.Fellowship)
        {
            InviterGuid = inviterGuid;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            //if (!response) return;

            var invited = Player;
            var inviter = PlayerManager.GetOnlinePlayer(InviterGuid);

            if (!response)
            {
                inviter?.SendMessage($"{invited.Name} {(timeout ? "did not respond to" : "has declined")} your offer of fellowship.");
                return;
            }

            if (invited != null && inviter != null && inviter.Fellowship != null)
                inviter.Fellowship.AddConfirmedMember(inviter, invited, response);
        }
    }

    public class Confirmation_SwearAllegiance : Confirmation
    {
        public ObjectGuid VassalGuid;

        public Confirmation_SwearAllegiance(ObjectGuid patronGuid, ObjectGuid vassalGuid)
            : base(patronGuid, ConfirmationType.SwearAllegiance)
        {
            VassalGuid = vassalGuid;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            //if (!response) return;

            var patron = Player;
            if (patron == null) return;

            var vassal = PlayerManager.GetOnlinePlayer(VassalGuid);

            if (!response)
            {
                vassal?.SendMessage($"{patron.Name} {(timeout ? "did not respond to" : "has declined")} your offer of allegiance.");
                return;
            }

            if (vassal != null)
                vassal.SwearAllegiance(patron.Guid.Full, true, true);
        }
    }

    public class Confirmation_YesNo: Confirmation
    {
        public ObjectGuid SourceGuid;

        public string Quest;

        public Confirmation_YesNo(ObjectGuid sourceGuid, ObjectGuid targetPlayerGuid, string quest)
            : base(targetPlayerGuid, ConfirmationType.Yes_No)
        {
            SourceGuid = sourceGuid;
            Quest = quest;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            var player = Player;
            if (player == null) return;

            var source = player.FindObject(SourceGuid, Player.SearchLocations.Landblock);

            if (source is Hook hook && hook.Item != null)
                source = hook.Item;

            if (source != null)
                source.EmoteManager.ExecuteEmoteSet(response ? EmoteCategory.TestSuccess : EmoteCategory.TestFailure, Quest, player);
        }
    }

    public class Confirmation_Custom: Confirmation
    {
        public Action Action;

        public Confirmation_Custom(ObjectGuid playerGuid, Action action)
            : base(playerGuid, ConfirmationType.Yes_No)
        {
            Action = action;
        }

        public override void ProcessConfirmation(bool response, bool timeout = false)
        {
            if (!response) return;

            var player = Player;
            if (player == null) return;

            Action();
        }
    }
}
