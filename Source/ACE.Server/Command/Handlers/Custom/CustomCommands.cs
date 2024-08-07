using ACE.Common;
using ACE.Database;
using ACE.Database.Models.World;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity.Actions;
using ACE.Server.Features.DailyXp;
using ACE.Server.Features.Discord;
using ACE.Server.Features.HotDungeons.Managers;
using ACE.Server.Features.Rifts;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Realms;
using ACE.Server.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACE.Server.Command.Handlers
{
    public static class CustomCommands
    {
        /** HotDungeons Start **/

        [CommandHandler("rifts", AccessLevel.Player, CommandHandlerFlag.None, 0, "Get a list of available dungeons.")]
        [CommandHandler("dungeons", AccessLevel.Player, CommandHandlerFlag.None, 0, "Get a list of available dungeons.")]
        public static void HandleCheckDungeons(ISession session, params string[] parameters)
        {
            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();

            message.Append("<Active Rift List>\n");
            message.Append("-----------------------\n");

            /*foreach (var rift in RiftManager.GetRifts())
            {
                var oreDropChance = rift.LandblockInstance.RealmRuleset.GetProperty(RealmPropertyInt.OreDropChance);
                var oreSlayerDropChance = rift.LandblockInstance.RealmRuleset.GetProperty(RealmPropertyInt.OreSlayerDropChance);
                var oreSalvageDropAmount = rift.LandblockInstance.RealmRuleset.GetProperty(RealmPropertyInt.OreSalvageDropAmount);
                Position.ParseInstanceID(rift.HomeInstance, out var isEphemeralRealm, out var realmId, out var instanceId);
                var realm = RealmManager.GetRealm(realmId, includeRulesets: true).Realm;
                message.Append($"Rift {rift.Name} from realm {realm.Name} is active!\n");
                message.Append($"With an xp bonus of {rift.BonuxXp.ToString("0.00")}x.\n");
                message.Append($"With an ore drop chance of 1/{oreDropChance}.\n");
                message.Append($"With an ore slayer drop chance of 1/{oreSlayerDropChance}.\n");
                message.Append($"With an ore salvage drop amount of {oreSalvageDropAmount * 2}.\n");
                message.Append("-----------------------\n");
            }*/

            message.Append("-----------------------\n");
            /*message.Append($"The Rift Entrance Portal in Subway (main hall, first room on the right) will bring you to a random Rift\n");
            message.Append($"<Time Remaining before reset: {DungeonManager.FormatTimeRemaining(DungeonManager.DungeonsTimeRemaining)}>\n");*/

            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");

            /*if (DungeonManager.DungeonsTimeRemaining.TotalMilliseconds <= 0)
                DungeonManager.Reset();*/
        }

        [CommandHandler("reset-dungeons", AccessLevel.Admin, CommandHandlerFlag.None, 0, "Get a list of available dungeons.")]
        public static void HandleResetDungeons(ISession session, params string[] paramters)
        {
            /*session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Resettinga Hot Dungeons>", ChatMessageType.System));
            DungeonManager.Reset(true);*/
        }

        [CommandHandler("dungeons-potential", AccessLevel.Developer, CommandHandlerFlag.None, 0, "Get a list of available potential dungeons.")]
        public static void HandleCheckDungeonsPotential(ISession session, params string[] paramters)
        {
            /*session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Active Potential Dungeon List>", ChatMessageType.System));
            foreach (var (realmId, dungeon) in DungeonManager.GetPotentialDungeons())
            {
                var realm = RealmManager.GetRealm(realmId, includeRulesets: true).Realm.Name;
                if (realm == null)
                    continue;

                var at = dungeon.Coords.Length > 0 ? $"at {dungeon.Coords}" : "";
                var message = $"Dungeon {dungeon.Name} from realm {realm} has potential {at}";
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n{message}", ChatMessageType.System));

                var xp = dungeon.TotalXpEarned;
                var playersTouched = dungeon.PlayerTouches;

                var xpMessage = $"--> Xp Earned: {Formatting.FormatIntWithCommas((uint)xp)}";
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n{xpMessage}", ChatMessageType.System));

                var playersTouchedMessage = $"--> Amount of creatures killed by players: {Formatting.FormatIntWithCommas(playersTouched)}";
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n{playersTouchedMessage}", ChatMessageType.System));
            }

            session.Network.EnqueueSend(new GameMessageSystemChat($"\nTime Remaining before reset: {DungeonManager.FormatTimeRemaining(DungeonManager.DungeonsTimeRemaining)}", ChatMessageType.System));
            */
        }

        /** Hot Dungeons End **/

        [CommandHandler("fi", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Resends all visible items and creatures to the client")]
        [CommandHandler("fixinvisible", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Resends all visible items and creatures to the client")]
        public static void HandleFixInvisible(ISession session, params string[] parameters)
        {
            session.Player.FixInvis();

        }

        /** Xp Cap Start **/
        [CommandHandler("reset-xp", AccessLevel.Admin, CommandHandlerFlag.None, 0, "Reset xp cap.")]
        public static void HandleResetXpCap(ISession session, params string[] parameters)
        {
            var playerName = "";
            if (parameters.Length > 0)
                playerName = string.Join(" ", parameters);

            if (!string.IsNullOrEmpty(playerName))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Resetting Daily Xp Cap for {playerName}>", ChatMessageType.System));
                DailyXpManager.ResetPlayersForDaily(playerName);
            }
            else
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Resetting Daily Xp Cap for all players>", ChatMessageType.System));
                DailyXpManager.ResetPlayersForDaily();
            }
        }

        [CommandHandler("show-xp", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show xp cap information.")]
        public static void HandleShowXp(ISession session, params string[] paramters)
        {
            var player = session.Player;
            if (!player.CurrentLandblock.RealmRuleset.GetProperty(RealmPropertyBool.CanEarnXp))
                return;

            session.Network.EnqueueSend(new GameMessageSystemChat($"\n<Showing Xp Cap Information>", ChatMessageType.System));
            var queryXp = player.QuestXp;
            var pvpXp = player.PvpXp;
            var monsterXp = player.MonsterXp;

            var hasPlayerLevelModifier = PropertyManager.GetBool("player_level_xp_modifier").Item;

            var globalAverageModifier = DailyXpManager.GetPlayerLevelXpModifier((int)player.Level).ToString("0.00");

            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The current week is {DailyXpManager.Week}.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The current day is {DailyXpManager.Day}.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The daily xp cap today for all players is {Formatting.FormatIntWithCommas(DailyXpManager.CurrentDailyXp.XpCap)}.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The current highest level player for the server is {(uint)DailyXpManager.MaxLevel}.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The most xp you can earn for a single category today is {Formatting.FormatIntWithCommas((ulong)player.DailyXpMaxPerCategory)}.", ChatMessageType.System));
            if (hasPlayerLevelModifier)
                session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> Your current global xp modifier is {globalAverageModifier}x.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> You have {Formatting.FormatIntWithCommas((ulong)player.DailyXpRemaining)} daily xp remaining.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> You have currently earned {Formatting.FormatIntWithCommas((ulong)queryXp)} quest xp for the day.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> You have currently earned {Formatting.FormatIntWithCommas((ulong)pvpXp)} pvp xp for the day.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> You have currently earned {Formatting.FormatIntWithCommas((ulong)monsterXp)} monster xp for the day.", ChatMessageType.System));
            session.Network.EnqueueSend(new GameMessageSystemChat($"\n--> The next daily xp reset will happen on {Formatting.FormatUtcToPst(DailyXpManager.CurrentDailyXp.EndTimeStamp)}.", ChatMessageType.System));
        }
        /** Xp Cap End **/

        /** Leaderboards/Stats Start **/
        [CommandHandler("topkills", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 kills leaderboard.")]
        [CommandHandler("leaderboards-kills", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 kills leaderboard.")]
        public static void HandleLeaderboardsKills(ISession session, params string[] parameters)
        {
            if (session != null)
            {
                if (session.AccessLevel == AccessLevel.Player && DateTime.UtcNow - session.Player.PrevLeaderboardPvPKillsCommandRequestTimestamp < TimeSpan.FromMinutes(1))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                    return;
                }
                session.Player.PrevLeaderboardPvPKillsCommandRequestTimestamp = DateTime.UtcNow;
            }

            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();

            message.Append("<Showing Top 10 Kills Leaderboard>\n");
            message.Append("-----------------------\n");

            var homeRealm = session?.Player?.HomeRealm != null ? (ushort)session.Player.HomeRealm : RealmManager.CurrentSeason.Realm.Id;
            var players = DatabaseManager.Pourtide.GetTopTenPlayersWithMostKills(homeRealm);

            for (var i = 0; i < players.Count; i++)
            {
                var stats = players[i];
                var player = PlayerManager.FindByGuid(stats.PlayerId);
                if (player == null)
                    continue;
                if (player.IsPendingDeletion || player.IsDeleted)
                    continue;
                message.Append($"{i + 1}. Name = {player.Name}, Kills = {stats.KillCount}\n");
            }

            message.Append("-----------------------\n");

            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");
        }

        /** Leaderboards/Stats Start **/
        [CommandHandler("my-kills", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show my kills.")]
        public static void HandlePersonalKills(ISession session, params string[] parameters)
        {
            if (session != null)
            {
                if (session.AccessLevel == AccessLevel.Player && DateTime.UtcNow - session.Player.PrevPersonalPvPKillsCommandRequestTimestamp < TimeSpan.FromMinutes(1))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                    return;
                }
                session.Player.PrevPersonalPvPKillsCommandRequestTimestamp = DateTime.UtcNow;
            }

            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();

            message.Append("<Showing Kill Count>\n");
            message.Append("-----------------------\n");

            var (count, last10Victims) = DatabaseManager.Pourtide.GetPersonalKillStats((uint)session.Player.Guid.Full);

            message.Append($"{session.Player.Name}, Total Kills = {count}\n");

            message.Append("-----------------------\n");
            message.Append("<Showing Last 10 Kills>\n");

            var victims = last10Victims
                .Select(guid => PlayerManager.FindByGuid(guid))
                .Where(player => player != null)
                .ToList();

            for (var i = 0; i < victims.Count; i++)
            {
                var victim = victims[i];
                message.Append($"{i + 1}. Name = {victim.Name}\n");
            }

            message.Append("-----------------------\n");

            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");
        }

        /** Leaderboards/Stats Start **/
        [CommandHandler("topdeaths", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 pvp deaths leaderboard.")]
        [CommandHandler("leaderboards-deaths", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 pvp deaths leaderboard.")]
        public static void HandleLeaderboardsDeaths(ISession session, params string[] parameters)
        {
            if (session != null)
            {
                if (session.AccessLevel == AccessLevel.Player && DateTime.UtcNow - session.Player.PrevLeaderboardPvPDeathsCommandRequestTimestamp < TimeSpan.FromMinutes(1))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                    return;
                }
                session.Player.PrevLeaderboardPvPDeathsCommandRequestTimestamp = DateTime.UtcNow;
            }

            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();

            message.Append("<Showing Top 10 Deaths Leaderboard>\n");
            message.Append("-----------------------\n");


            var homeRealm = session?.Player?.HomeRealm != null ? (ushort)session.Player.HomeRealm : RealmManager.CurrentSeason.Realm.Id;
            var players = DatabaseManager.Pourtide.GetPlayerWithMostDeaths(homeRealm);

            for (var i = 0; i < players.Count; i++)
            {
                var stats = players[i];
                var player = PlayerManager.FindByGuid(stats.PlayerId);
                if (player == null)
                    continue;
                if (player.IsPendingDeletion || player.IsDeleted)
                    continue;
                message.Append($"{i + 1}. Name = {player.Name}, Deaths = {stats.DeathCount}\n");
            }

            message.Append("-----------------------\n");

            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");

        }

        [CommandHandler("topdamage", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 5 damage dealers for all three combat modes.")]
        [CommandHandler("leaderboards-damage", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 5 damage dealers for all three combat modes.")]
        public static void HandleLeaderboardsDamage(ISession session, params string[] parameters)
        {
            if (session != null)
            {
                if (session.AccessLevel == AccessLevel.Player && DateTime.UtcNow - session.Player.PrevLeaderboardPvPDamageCommandRequestTimestamp < TimeSpan.FromMinutes(1))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                    return;
                }
                session.Player.PrevLeaderboardPvPDamageCommandRequestTimestamp = DateTime.UtcNow;
            }

            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();


            var homeRealm = session?.Player?.HomeRealm != null ? (ushort)session.Player.HomeRealm : RealmManager.CurrentSeason.Realm.Id;
            var meleePlayers = DatabaseManager.Pourtide.GetPlayersWithMostDamage(homeRealm, (uint)WorldObjects.CombatType.Melee);
            var missilePlayers = DatabaseManager.Pourtide.GetPlayersWithMostDamage(homeRealm, (uint)WorldObjects.CombatType.Missile);
            var magicPlayers = DatabaseManager.Pourtide.GetPlayersWithMostDamage(homeRealm, (uint)WorldObjects.CombatType.Magic);

            message.Append("<Showing Top Magic Damage Leaderboard>\n");
            message.Append("-----------------------\n");

            for (var i = 0; i < magicPlayers.Count; i++)
            {
                var stats = magicPlayers[i];
                var player = PlayerManager.FindByGuid(stats.PlayerId);
                if (player == null)
                    continue;
                message.Append($"{i + 1}. Name = {player.Name}, TotalDamage = {stats.TotalDamage}\n");
            }

            message.Append("-----------------------\n");

            message.Append("<Showing Top Melee Damage Leaderboard>\n");
            message.Append("-----------------------\n");

            for (var i = 0; i < meleePlayers.Count; i++)
            {
                var stats = meleePlayers[i];
                var player = PlayerManager.FindByGuid(stats.PlayerId);
                if (player == null)
                    continue;
                message.Append($"{i + 1}. Name = {player.Name}, TotalDamage = {stats.TotalDamage}\n");
            }

            message.Append("-----------------------\n");

            message.Append("<Showing Top Missile Damage Leaderboard>\n");
            message.Append("-----------------------\n");

            for (var i = 0; i < missilePlayers.Count; i++)
            {
                var stats = missilePlayers[i];
                var player = PlayerManager.FindByGuid(stats.PlayerId);
                if (player == null)
                    continue;
                message.Append($"{i + 1}. Name = {player.Name}, TotalDamage = {stats.TotalDamage}\n");
            }

            message.Append("-----------------------\n");
            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");

        }


        [CommandHandler("topxp", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 Levels leaderboard.")]
        [CommandHandler("leaderboards-Xp", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show top 10 Levels leaderboard.")]
        public static void HandleLeaderboardsXp(ISession session, params string[] parameters)
        {
            if (session != null)
            {
                if (session.AccessLevel == AccessLevel.Player && DateTime.UtcNow - session.Player.PrevLeaderboardXPCommandRequestTimestamp < TimeSpan.FromMinutes(1))
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                    return;
                }
                session.Player.PrevLeaderboardXPCommandRequestTimestamp = DateTime.UtcNow;
            }

            ulong discordChannel = 0;
            if (parameters.Length > 1 && parameters[0] == "discord")
                ulong.TryParse(parameters[1], out discordChannel);

            StringBuilder message = new StringBuilder();

            message.Append("<Showing Top 10 Xp Leaderboard>\n");
            message.Append("-----------------------\n");

            var players = PlayerManager.GetAllPlayers()
                .Where(player => {
                    var homeRealm = player.GetProperty(PropertyInt.HomeRealm);
                    return player.Account.AccessLevel == (uint)AccessLevel.Player &&
                    homeRealm != null &&
                    (ushort)homeRealm == RealmManager.CurrentSeason.Realm.Id;
                })
                .OrderByDescending(player => player.Level)
                .Take(10)
                .ToList();

            for (var i = 0; i < players.Count; i++)
            {
                var info = players[i];
                var player = PlayerManager.FindByGuid(info.Guid);
                if (player == null)
                    continue;
                if (player.IsPendingDeletion || player.IsDeleted)
                    continue;

                message.Append($"{i + 1}. Name = {player.Name}, Level = {player.Level}\n");
            }

            message.Append("-----------------------\n");

            if (discordChannel == 0)
                CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
            else
                DiscordChatBridge.SendMessage(discordChannel, $"`{message.ToString()}`");

        }

        /** Leadearboards/Stats End **/

        /** Player Utility Commands Start **/
        [CommandHandler("fl", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Force log off of a character that's stuck in game. Is only allowed when initiated from a character that is on the same account as the target character.")]
        [CommandHandler("ForceLogoffStuckCharacter", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Force log off of character that's stuck in game. Is only allowed when initiated from a character that is on the same account as the target character.")]
        public static void HandleForceLogoffStuckCharacter(ISession session, params string[] parameters)
        {
            try
            {
                if (session == null)
                    return;

                var playerName = "";
                if (parameters.Length > 0)
                    playerName = string.Join(" ", parameters);

                Player target = null;

                if (!string.IsNullOrEmpty(playerName))
                {
                    var plr = PlayerManager.FindByName(playerName);
                    if (plr != null)
                    {
                        target = PlayerManager.GetOnlinePlayer(plr.Guid);

                        if (target == null)
                        {
                            CommandHandlerHelper.WriteOutputInfo(session, $"Unable to force log off for {plr.Name}: Player is not online.");
                            return;
                        }

                        //Verify the target is not the current player
                        if (session.Player.Guid == target.Guid)
                        {
                            CommandHandlerHelper.WriteOutputInfo(session, $"Unable to force log off for {plr.Name}: You cannot target yourself, please try with a different character on same account.");
                            return;
                        }

                        //Verify the target is on the same account as the current player
                        if (session.AccountId != target.Account.AccountId)
                        {
                            CommandHandlerHelper.WriteOutputInfo(session, $"Unable to force log off for {plr.Name}: Target must be within same account as the player who issues the logoff command. Please reach out for admin support.");
                            return;
                        }

                        DeveloperCommands.HandleForceLogoff(session, parameters);
                    }
                    else
                    {
                        CommandHandlerHelper.WriteOutputInfo(session, $"Unable to force log off for {playerName}: Player not found.");
                        return;
                    }
                }
                else
                {
                    CommandHandlerHelper.WriteOutputInfo(session, $"Invalid parameters, please provide a player name for the player that needs to be logged off.");
                    return;
                }

            }
            catch (Exception ex)
            {
                CommandHandlerHelper.WriteOutputError(session, $"Error: Failed to force logout player");
                CommandHandlerHelper.WriteOutputError(session, ex.Message);
                CommandHandlerHelper.WriteOutputError(session, System.Environment.StackTrace);
            }
        }

        /// <summary>
        /// List online players within the character's allegiance.
        /// </summary>
        [CommandHandler("who", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "List online players within the character's allegiance.")]
        public static void HandleWho(ISession session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("command_who_enabled").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"who\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            if (session.Player.MonarchId == null)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("You must be in an allegiance to use this command.", ChatMessageType.Broadcast));
                return;
            }

            if (DateTime.UtcNow - session.Player.PrevWho < TimeSpan.FromMinutes(1))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("You have used this command too recently!", ChatMessageType.Broadcast));
                return;
            }

            session.Player.PrevWho = DateTime.UtcNow;

            StringBuilder message = new StringBuilder();
            message.Append("Allegiance Members: \n");


            uint playerCounter = 0;
            foreach (var player in PlayerManager.GetAllOnline().OrderBy(p => p.Name))
            {
                if (player.MonarchId == session.Player.MonarchId)
                {
                    message.Append($"{player.Name} - Level {player.Level}\n");
                    playerCounter++;
                }
            }

            message.Append("Total: " + playerCounter + "\n");

            CommandHandlerHelper.WriteOutputInfo(session, message.ToString(), ChatMessageType.Broadcast);
        }
        /** Player Utility Commands End **/

        [CommandHandler("track-bounty", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Get bounty information from Pour Collector")]
        public static void HandleTrackBounty(ISession session, params string[] paramters)
        {
            session.Player.TrackBounty();
        }

        [CommandHandler("reload-all-landblocks", AccessLevel.Admin, CommandHandlerFlag.None, 0, "Reloads all landblocks currently loaded.")]
        public static void HandleReloadAllLandblocks(ISession session, params string[] parameters)
        {
            ActionChain lbResetChain = new ActionChain();
            var lbs = LandblockManager.GetLoadedLandblocks().Select(x => (id: x.Id, instance: x.Instance));
            var enumerator = lbs.GetEnumerator();

            ActionEventDelegate resetLandblockAction = null;
            resetLandblockAction = new ActionEventDelegate(() =>
            {
                if (!enumerator.MoveNext())
                    return;
                if (LandblockManager.IsLoaded(enumerator.Current.id, enumerator.Current.instance))
                {
                    var lb = LandblockManager.GetLandblockUnsafe(enumerator.Current.id, enumerator.Current.instance);
                    if (lb != null)
                    {
                        if (session?.Player?.CurrentLandblock != lb)
                            CommandHandlerHelper.WriteOutputInfo(session, $"Reloading 0x{lb.LongId:X16}", ChatMessageType.Broadcast);
                        lb.Reload();
                    }
                }
                lbResetChain.AddDelayForOneTick();
                lbResetChain.AddAction(WorldManager.ActionQueue, resetLandblockAction);
            });
            lbResetChain.AddAction(WorldManager.ActionQueue, resetLandblockAction);
            lbResetChain.EnqueueChain();
        }

        [CommandHandler("scarabs", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Add scarabs to inventory.")]
        public static void HandleAddScarabs(ISession session, params string[] parameters)
        {
            if (!session.Player.CurrentLandblock.RealmRuleset.GetProperty(RealmPropertyBool.IsDuelingRealm))
                return;

            DuelRealmHelpers.AddScarabsToInventory(session.Player);
        }

        private static List<string> MorphGems = new List<string>()
        {
            "slow",
            "spell-chain",
            "root"
        };

        [CommandHandler("create-morph-gem", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Create slow morph gem.")]
        public static void HandleCreateMorphGem(ISession session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("test_server").Item)
                return;

            var gemName = "";
            if (parameters.Length > 0)
                gemName = string.Join(" ", parameters);

            if (string.IsNullOrEmpty(gemName))
                return;

            // Create a dummy treasure profile for passing in tier value
            TreasureDeath dt = new TreasureDeath
            {
                Tier = 5,
                LootQualityMod = 0,
                CantripAmount = MutationsManager.CantripRoll(),
                MagicItemTreasureTypeSelectionChances = 9,  // 8 or 9?
            };

            if (MorphGems.Contains(gemName))
            {
                Gem gem = null;
                switch (gemName)
                {
                    case "slow":
                        gem = session.Player.CurrentLandblock.RealmRuleset.LootGenerationFactory.CreateSlowMorphGem(dt, true);
                        break;
                    case "spell-chain":
                        gem = session.Player.CurrentLandblock.RealmRuleset.LootGenerationFactory.CreateSpellChainMorphGem(dt, true);
                        break;
                    case "root":
                        gem = session.Player.CurrentLandblock.RealmRuleset.LootGenerationFactory.CreateRootMorphGem(dt, true);
                        break;
                }

                if (gem != null)
                    session.Player.TryCreateInInventoryWithNetworking(gem);
            }
        }

        [CommandHandler("forcepk", AccessLevel.Envoy, CommandHandlerFlag.RequiresWorld, 0, "force an npk player to be pk")]
        public static void HandleForcePk(ISession session, params string[] parameters)
        {
            var objectId = ObjectGuid.Invalid;

            var target = session.Player.CurrentAppraisalTarget;

            if (target.HasValue)
                objectId = new ObjectGuid((uint)session.Player.CurrentAppraisalTarget);

            var wo = session.Player.CurrentLandblock?.GetObject(objectId);

            if (wo is null)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Unable to locate what you have selected.", ChatMessageType.Broadcast));
            }
            else if (wo is Player player && player.IsNPK)
            {
                player.MinimumTimeSincePk = PropertyManager.GetDouble("pk_respite_timer").Item;
            }
        }

        [CommandHandler("player-to-ip-map", AccessLevel.Admin, CommandHandlerFlag.RequiresWorld, 0, "ip print the player to ip guid map")]
        public static void HandlePlayerToIpMap(ISession session, params string[] parameters)
        {
            PlayerManager.PrintIptoPlayerGuidMap();
        }

        [CommandHandler("adjust-daily-xp", AccessLevel.Admin, CommandHandlerFlag.RequiresWorld, 1, "adjusts the daily xp by hours, ex: -1 would reduce the end timestamp by 1 hour, 1 would increase the end timestamp by 1 hour")]
        public static void HandleAdjustDailyXp(ISession session, params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("Invalid number of parameters. Usage: adjust-daily-xp <hours>", ChatMessageType.Broadcast));
                return;
            }

            if (!int.TryParse(parameters[0], out int hours))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("Invalid parameter. Please provide a valid number of hours.", ChatMessageType.Broadcast));
                return;
            }

            try
            {
                DailyXpManager.AdjustDailyXpCapsTimestamps(hours);
                session.Network.EnqueueSend(new GameMessageSystemChat($"Daily XP caps timestamps adjusted by {hours} hours", ChatMessageType.Broadcast));
            }
            catch (Exception ex)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"An error occurred while adjusting timestamps: {ex.Message}", ChatMessageType.Broadcast));
            }

        }
    }
}
