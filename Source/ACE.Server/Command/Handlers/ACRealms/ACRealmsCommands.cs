using ACE.Common;
using ACE.Database;
using ACE.Database.Models.Shard;
using ACE.Entity;
using ACE.Entity.ACRealms;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Features.Discord;
using ACE.Server.Features.HotDungeons.Managers;
using ACE.Server.Features.Rifts;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Physics.Common;
using ACE.Server.Realms;
using ACE.Server.WorldObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ACE.Server.Command.Handlers
{
    public static class ACRealmsCommands
    {
        [CommandHandler("season-info", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Get info about the current season your character belongs to.")]
        public static void HandleSeasonInfo(ISession session, params string[] paramters)
        {
            var player = session?.Player;
            var season = RealmManager.CurrentSeason;

            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Season Information>", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{season.Realm.Name} - Id: {season.Realm.Id} - Instance: {season.StandardRules.GetDefaultInstanceID(player, player.Location.AsLocalPosition())}", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{season.StandardRules.DebugOutputString()}", ChatMessageType.System));
        }

        [CommandHandler("season-list", AccessLevel.Player, CommandHandlerFlag.None, 0, "Get a list of available seasons to choose from.")]
        public static void HandleSeasonList(ISession session, params string[] paramters)
        {
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Season List>", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{RealmManager.GetSeasonList()}", ChatMessageType.System));
        }

        [CommandHandler("realm-list", AccessLevel.Player, CommandHandlerFlag.None, 0, "Get a list of available realms stored in RealmManager.")]
        public static void HandleRealmList(ISession session, params string[] paramters)
        {
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Realm List>", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{RealmManager.GetRealmList()}", ChatMessageType.System));
        }

        [CommandHandler("ruleset-list", AccessLevel.Player, CommandHandlerFlag.None, 0, "Get a list of available rulesets stored in RealmManager.")]
        public static void HandleRulesetList(ISession session, params string[] paramters)
        {
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Ruleset List>", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{RealmManager.GetRulesetsList()}", ChatMessageType.System));

        }

        [CommandHandler("telerealm", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, 0, "Teleports the current player to another realm.")]
        public static void HandleMoveRealm(ISession session, params string[] parameters)
        {
            if (parameters.Length < 1)
                return;
            if (!ushort.TryParse(parameters[0], out var realmid))
                return;

            var pos = session.Player.Location;
            var newpos = new InstancedPosition(pos, InstancedPosition.InstanceIDFromVars(realmid, 0, false));

            session.Player.Teleport(newpos);
            var positionMessage = new GameMessageSystemChat($"Teleporting to realm {realmid}.", ChatMessageType.Broadcast);
            session.Network.EnqueueSend(positionMessage);
        }

        [CommandHandler("realm-info", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Lists all properties for the current realm.")]
        public static void HandleZoneInfo(ISession session, params string[] parameters)
        {
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Realm Information>", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{session.Player.CurrentLandblock.RealmRuleset.Realm.Name} - Id: {session.Player.CurrentLandblock.RealmRuleset.Realm.Id} - Instance: {session.Player.Location.Instance} ", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n{session.Player.CurrentLandblock.RealmRuleset.DebugOutputString()}", ChatMessageType.System));
        }

        [CommandHandler("exitinstance", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleExitInstance(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }

        [CommandHandler("exitinst", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleExitInst(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }


        [CommandHandler("exiti", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleExitI(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }

        [CommandHandler("leaveinstance", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleLeaveInstance(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }

        [CommandHandler("leaveinst", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleLeaveInst(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }

        [CommandHandler("leavei", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, "Leaves the current instance, if the player is currently in one.")]
        public static void HandleLeaveI(ISession session, params string[] parameters)
        {
            session.Player.ExitInstance();
        }

        [CommandHandler("compile-ruleset", AccessLevel.Admin, CommandHandlerFlag.RequiresWorld, 1, "Gives a diagnostic trace of a ruleset compilation for the current landblock",
                    "(required) { full | landblock | ephemeral-new | ephemeral-cached | all }\n" +
                    "(optional) random seed")]
        public static void HandleCompileRuleset(ISession session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("acr_enable_ruleset_seeds").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"The server property 'acr_enable_ruleset_seeds' must be enabled to use this command.", ChatMessageType.Broadcast));
                return;
            }

            string type = parameters[0];
            int seed;
            if (parameters.Length > 1)
            {
                if (!int.TryParse(parameters[1], out seed))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Invalid random seed, must pass an integer", ChatMessageType.Broadcast));
                    return;
                }
            }
            else
                seed = Random.Shared.Next();

            string result;
            switch (type)
            {
                case "all":
                    HandleCompileRuleset(session, "landblock", seed.ToString());
                    if (session.Player.CurrentLandblock.IsEphemeral)
                    {
                        HandleCompileRuleset(session, "ephemeral-cached", seed.ToString());
                        HandleCompileRuleset(session, "ephemeral-new", seed.ToString());
                    }
                    HandleCompileRuleset(session, "full", seed.ToString());
                    return;
                default:
                    result = CompileRulesetRaw(session, seed, type);
                    break;
            }

            var filename = $"compile-ruleset-output-{session.Player.Name}-{type}.txt";
            File.WriteAllText(filename, result);
            session.Network.EnqueueSend(new GameMessageSystemChat($"Logged compilation output to {filename}", ChatMessageType.Broadcast));
        }

        public class InvalidCommandException() : Exception { }
        public static string CompileRulesetRaw(ISession session, int seed, string type, DateTime? timeContext = null)
        {
            Ruleset ruleset;
            var ctx = Ruleset.MakeDefaultContext().WithTrace(deriveNewSeedEachPhase: false).WithNewSeed(seed);
            if (timeContext.HasValue)
                ctx = ctx.WithTimeContext(timeContext.Value);

            switch (type)
            {
                case "landblock":
                    ruleset = AppliedRuleset.MakeRerolledRuleset(session.Player.RealmRuleset.Template, ctx);
                    break;
                case "ephemeral-new":
                    if (!session.Player.CurrentLandblock.IsEphemeral)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"The current landblock is not ephemeral.", ChatMessageType.Broadcast));
                        throw new InvalidCommandException();
                    }
                    ruleset = AppliedRuleset.MakeRerolledRuleset(session.Player.CurrentLandblock.InnerRealmInfo.RulesetTemplate.RebuildTemplateWithContext(ctx), ctx);
                    break;
                case "ephemeral-cached":
                    if (!session.Player.CurrentLandblock.IsEphemeral)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"The current landblock is not ephemeral.", ChatMessageType.Broadcast));
                        throw new InvalidCommandException();
                    }
                    ruleset = AppliedRuleset.MakeRerolledRuleset(session.Player.RealmRuleset.Template, ctx);
                    break;
                case "full":
                    RulesetTemplate template;
                    if (!session.Player.CurrentLandblock.IsEphemeral)
                        template = RealmManager.BuildRuleset(session.Player.RealmRuleset.Realm, null, ctx);
                    else
                        template = session.Player.CurrentLandblock.InnerRealmInfo.RulesetTemplate.RebuildTemplateWithContext(ctx);
                    ruleset = AppliedRuleset.MakeRerolledRuleset(template, ctx);
                    break;
                default:
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Unknown compilation type.", ChatMessageType.Broadcast));
                    throw new InvalidCommandException();
            }
            return ruleset.Context.FlushLog();
        }

        [CommandHandler("ruleset-seed", AccessLevel.Envoy, CommandHandlerFlag.RequiresWorld, 0, "Shows the randomization seed for the current landblock's ruleset")]
        public static void HandleRulesetSeed(ISession session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("acr_enable_ruleset_seeds").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"The server property 'acr_enable_ruleset_seeds' must be enabled to use this command.", ChatMessageType.Broadcast));
                return;
            }

            session.Network.EnqueueSend(new GameMessageSystemChat($"Ruleset seed: {session.Player.RealmRuleset.Context.RandomSeed}", ChatMessageType.Broadcast));
        }

        [CommandHandler("spl", AccessLevel.Developer, CommandHandlerFlag.None, 0, "Show player locations.")]
        [CommandHandler("show-player-locations", AccessLevel.Developer, CommandHandlerFlag.RequiresWorld, 0, "Show player locations.")]
        public static void HandleShowPlayerLocations(ISession session, params string[] parameters)
        {

            foreach (var player in PlayerManager.GetAllOnline())
            {
                if (player != null && player.Location != null)
                {
                    RiftManager.TryGetActiveRift(player.HomeRealm, player.Location.LandblockHex, out Rift rift);
                    DungeonManager.TryGetDungeonLandblock(player.Location.LandblockHex, out DungeonLandblock dungeon);

                    var at = rift != null ? $"Rift {rift.Name}" : dungeon != null ? $"Dungeon {dungeon.Name}" : player.Location.GetMapCoordStr();
                    CommandHandlerHelper.WriteOutputInfo(session, $"Name = {player.Name}, At = {at}, RealmId = {player.Location.RealmID}, Instance = {player.Location.Instance} ", ChatMessageType.WorldBroadcast);
                }
            }
        }

        // Requires IsDuelingRealm and HomeRealm to be set
        [CommandHandler("rebuff", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0,
            "Buffs you with all beneficial spells. Only usable in certain realms.")]
        public static void HandleRebuff(ISession session, params string[] parameters)
        {
            var player = session.Player;
            var realm = RealmManager.GetRealm(player.HomeRealm, includeRulesets: false);
            if (realm == null) return;
            if (!realm.StandardRules.GetProperty(RealmPropertyBool.IsDuelingRealm)) return;
            var ts = player.GetProperty(PropertyInt.LastRebuffTimestamp);
            if (ts != null)
            {
                var timesince = (int)Time.GetUnixTime() - ts.Value;
                if (timesince < 180)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You may use this command again in {180 - timesince}s.", ChatMessageType.Broadcast));
                    return;
                }
            }
            player.SetProperty(PropertyInt.LastRebuffTimestamp, (int)Time.GetUnixTime());
            player.CreateSentinelBuffPlayers(new Player[] { player }, true);
        }
    }

}
