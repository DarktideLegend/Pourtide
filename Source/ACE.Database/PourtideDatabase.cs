using ACE.Database.Models.Auth;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ACE.Database.Models.Pourtide;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ACE.Database
{
    public class PourtideDatabase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IDbContextFactory<PourtideDbContext> ContextFactory { get; }

        public PourtideDatabase(IServiceProvider services)
        {
            ContextFactory = services.GetRequiredService<IDbContextFactory<PourtideDbContext>>();
        }

        public bool Exists(bool retryUntilFound)
        {
            var config = Common.ConfigManager.Config.MySql.Pourtide;

            for (; ; )
            {
                using (var context = ContextFactory.CreateDbContext())
                {
                    if (((RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>()).Exists())
                    {
                        log.DebugFormat("Successfully connected to {0} database on {1}:{2}.", config.Database, config.Host, config.Port);
                        return true;
                    }
                }

                log.Error($"Attempting to reconnect to {config.Database} database on {config.Host}:{config.Port} in 5 seconds...");

                if (retryUntilFound)
                    Thread.Sleep(5000);
                else
                    return false;
            }
        }

        public (DateTime Daily, DateTime Weekly, uint Week) UpdateXpCap()
        {
            using (var context = new PourtideDbContext())
            {
                var timestamps = context.XpCap.FirstOrDefault();

                DateTime currentDateTime = DateTime.UtcNow;

                if (currentDateTime > timestamps.DailyTimestamp)
                {
                    timestamps.DailyTimestamp = currentDateTime.AddDays(1);
                }

                if (currentDateTime > timestamps.WeeklyTimestamp)
                {
                    timestamps.WeeklyTimestamp = currentDateTime.AddDays(7);

                    timestamps.Week++;
                }

                context.SaveChanges();

                return (timestamps.DailyTimestamp, timestamps.WeeklyTimestamp, timestamps.Week);
            }
        }

        public (DateTime Daily, DateTime Weekly, uint Week) GetXpCapTimestamps()
        {
            using (var context = new PourtideDbContext())
            {
                var timestamps = context.XpCap.FirstOrDefault();

                if (timestamps == null)
                {
                    timestamps = new XpCap
                    {
                        DailyTimestamp = DateTime.UtcNow.AddDays(1),
                        WeeklyTimestamp = DateTime.UtcNow.AddDays(7),
                        Week = 1
                    };

                    context.XpCap.Add(timestamps);
                    context.SaveChanges();
                }

                return (timestamps.DailyTimestamp, timestamps.WeeklyTimestamp, timestamps.Week);
            }
        }

        public Dictionary<ushort, Dictionary<string, HashSet<ulong>>> GetIpToCharacterLoginMap()
        {
            using (var context = new PourtideDbContext())
            {
                var characterLogins = context.CharacterLogin.ToList();
                var ipCharacterMap = characterLogins
                    .GroupBy(cl => cl.HomeRealmId)
                    .ToDictionary(
                        group => group.Key,
                        group => group
                            .GroupBy(cl => cl.SessionIP)
                            .ToDictionary(
                                innerGroup => innerGroup.Key,
                                innerGroup => new HashSet<ulong>(innerGroup.Select(cl => cl.CharacterId))
                            )
                    );

                return ipCharacterMap;
            }
        }

        public void LogCharacterLogin(ushort homeRealmId, ushort currentRealmId, uint accountId, string accountName, string sessionIP, ulong characterId, string characterName)
        {
            var logEntry = new CharacterLogin();

            try
            {
                logEntry.AccountId = accountId;
                logEntry.AccountName = accountName;
                logEntry.SessionIP = sessionIP;
                logEntry.CharacterId = characterId;
                logEntry.CharacterName = characterName;
                logEntry.HomeRealmId = homeRealmId;
                logEntry.CurrentRealmId = currentRealmId;
                logEntry.LoginDateTime = DateTime.Now;

                using (var context = new PourtideDbContext())
                {
                    context.CharacterLogin.Add(logEntry);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Exception in LogCharacterLogin saving character login info to DB. Ex: {ex}");
            }
        }
        public List<string> GetCharactersAssociatedWithIp(string sessionIp)
        {
            using (var context = new PourtideDbContext())
            {
                var logins = context.CharacterLogin.Where(login => login.SessionIP == sessionIp);
                return logins.Select(login => login.CharacterName).ToList();
            }
        }

        public PKStatsKill TrackPkStatsKill(ushort homeRealmId, ushort currentRealmId, ulong killerId, ulong victimId)
        {
            using (var context = new PourtideDbContext())
            {
                var kill = new PKStatsKill()
                {
                    KillerId = killerId,
                    VictimId = victimId,
                    HomeRealmId = homeRealmId,
                    CurrentRealmId = currentRealmId,
                    EventTime = DateTime.UtcNow,
                };

                context.PKStatsKills.Add(kill);
                context.SaveChanges();
                return kill;
            }
        }
        public void TrackPkStatsDamage(ushort homeRealmId, ushort currentRealmId, uint attackerId, uint defenderId, int damageAmount, bool isCrit, uint combatType)
        {
            if (damageAmount <= 0)
                return;

            using (var context = new PourtideDbContext())
            {
                var newPKStatsDamage = new PKStatsDamage
                {
                    AttackerId = attackerId,
                    DefenderId = defenderId,
                    DamageAmount = damageAmount,
                    HomeRealmId = homeRealmId,
                    CurrentRealmId = currentRealmId,
                    IsCrit = isCrit,
                    CombatMode = combatType,
                    EventTime = DateTime.Now
                };

                context.PKStatsDamages.Add(newPKStatsDamage);

                context.SaveChanges();
            }
        }

        public (ulong PlayerId, int KillCount) GetPlayerWithMostKills()
        {
            using (var context = new PourtideDbContext())
            {
                var playerKills = context.PKStatsKills
                    .GroupBy(kill => kill.KillerId)
                    .Select(group => new
                    {
                        PlayerId = group.Key,
                        KillCount = group.Count()
                    })
                    .OrderByDescending(x => x.KillCount)
                    .FirstOrDefault();

                return (playerKills?.PlayerId ?? 0, playerKills?.KillCount ?? 0);
            }
        }

        public class PlayerInfo
        {
            public uint Id { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
        }

        public List<(uint PlayerId, int DeathCount)> GetPlayerWithMostDeaths(ushort realmId)
        {
            using (var context = new PourtideDbContext())
            {
                var topTenPlayers = context.PKStatsKills
                    .Where(stats => stats.HomeRealmId == realmId)
                    .GroupBy(kill => kill.VictimId)
                    .Select(group => new
                    {
                        PlayerId = group.Key,
                        DeathCount = group.Count()
                    })
                    .OrderByDescending(x => x.DeathCount)
                    .Take(10)
                    .ToList();

                return topTenPlayers.Select(player => ((uint)player.PlayerId, player.DeathCount)).ToList();
            }
        }
        public class TopDamagePlayer
        {
            public uint PlayerId { get; set; }
            public int TotalDamage { get; set; }
        }

        public List<TopDamagePlayer> GetPlayersWithMostDamage(ushort realmId, uint combatType, int max = 5)
        {
            using (var context = new PourtideDbContext())
            {
                var topPlayers = context.PKStatsDamages
                    .Where(stats => stats.HomeRealmId == realmId && stats.CombatMode == combatType)
                    .GroupBy(stats => stats.AttackerId)
                    .Select(group => new TopDamagePlayer
                    {
                        PlayerId = group.Key,
                        TotalDamage = group.Sum(d => d.DamageAmount)
                    })
                    .OrderByDescending(damager => damager.TotalDamage)
                    .Take(max)
                    .ToList();

                return topPlayers;
            }
        }

        public List<(uint PlayerId, int KillCount)> GetTopTenPlayersWithMostKills(ushort realmId)
        {
            using (var context = new PourtideDbContext())
            {
                var topTenPlayers = context.PKStatsKills
                    .Where(stats => stats.HomeRealmId == realmId)
                    .GroupBy(kill => kill.KillerId)
                    .Select(group => new
                    {
                        PlayerId = group.Key,
                        KillCount = group.Count()
                    })
                    .OrderByDescending(x => x.KillCount)
                    .Take(10)
                    .ToList();

                return topTenPlayers.Select(player => ((uint)player.PlayerId, player.KillCount)).ToList();
            }
        }

        public (int killCount, List<uint>) GetPersonalKillStats(uint playerId)
        {
            using (var context = new PourtideDbContext())
            {
                var last10Kills = context.PKStatsKills
                  .Where(kill => kill.KillerId == playerId)
                  .OrderByDescending(kill => kill.EventTime)
                  .Take(10)
                  .ToList();

                var last10VictimIds = last10Kills.Select(kill => (uint)kill.VictimId).ToList();

                var killCount = context.PKStatsKills
                    .Where(kill => kill.KillerId == playerId)
                    .Count();

                return (killCount, last10VictimIds);
            }
        }

        public PKStatsKill GetLastKillEntry(uint killerId, uint victimId)
        {
            using (var context = new PourtideDbContext())
            {
                return context.PKStatsKills
                    .Where(kill => kill.KillerId == killerId && kill.VictimId == victimId)
                    .OrderByDescending(kill => kill.EventTime)
                    .FirstOrDefault();
            }

        }

        public bool HasPkTophyCooldownExpired(ulong killerId, ulong victimId)
        {
            using (var context = new PourtideDbContext())
            {
                var trophyCooldown = context.PkTrophyCooldowns
                    .FirstOrDefault(tc => tc.KillerId == killerId && tc.VictimId == victimId);

                if (trophyCooldown != null)
                {
                    return trophyCooldown.CooldownEndTime < DateTime.Now;
                }

                return true;
            }
        }

        public bool UpdatePkTrophyCooldown(ulong killerId, ulong victimId)
        {
            using (var context = new PourtideDbContext())
            {
                // Check if a PK trophy cooldown exists for the killerId and victimId pair
                var trophyCooldown = context.PkTrophyCooldowns
                    .FirstOrDefault(tc => tc.KillerId == killerId && tc.VictimId == victimId);

                if (trophyCooldown != null)
                {
                    // Check if the cooldown has expired
                    if (trophyCooldown.CooldownEndTime < DateTime.Now)
                    {
                        // Cooldown has expired, update the cooldown end time
                        trophyCooldown.CooldownEndTime = DateTime.Now.AddMinutes(20);
                        context.SaveChanges();

                        // Return true to indicate that the cooldown has expired
                        return true;
                    }
                    else
                    {
                        // Cooldown has not expired
                        return false;
                    }
                }
                else
                {
                    trophyCooldown = new PkTrophyCooldown()
                    {
                        KillerId = killerId,
                        VictimId = victimId,
                    };
                    trophyCooldown.CooldownEndTime = DateTime.Now.AddMinutes(20);
                    context.PkTrophyCooldowns.Add(trophyCooldown);
                    context.SaveChanges();
                    // No existing cooldown, return false
                    return true;
                }
            }
        }

        public void ResetDailyXpCaps(Dictionary<(uint, uint), ulong> dailyXpData)
        {
            using (var context = new PourtideDbContext())
            {
                var oldEntries = context.DailyXpCaps.ToList();
                context.DailyXpCaps.RemoveRange(oldEntries);

                DateTime currentDate = DateTime.UtcNow;
                foreach (var entry in dailyXpData)
                {
                    var week = entry.Key.Item1;
                    var day = entry.Key.Item2;
                    var xp = entry.Value;

                    // Calculate start and end timestamps
                    var startTimestamp = GetStartTimestamp(currentDate, week, day);
                    var endTimestamp = GetEndTimestamp(startTimestamp);

                    var dailyXpCap = new DailyXpCap
                    {
                        Week = week,
                        Day = day,
                        DailyXp = xp,
                        StartTimestamp = startTimestamp,
                        EndTimestamp = endTimestamp
                    };

                    context.DailyXpCaps.Add(dailyXpCap);
                }

                context.SaveChanges();
            }
        }

        private DateTime GetStartTimestamp(DateTime baseDate, uint week, uint day)
        {
            DateTime startDate = baseDate.Date.AddDays((week - 1) * 7 + (day - 1));

            DateTime startTimestamp = startDate.AddHours(baseDate.Hour).AddMinutes(baseDate.Minute).AddSeconds(baseDate.Second);

            return startTimestamp;
        }

        private DateTime GetEndTimestamp(DateTime startTimestamp)
        {
            return startTimestamp.AddDays(1).AddTicks(-1);
        }

        public void AdjustDailyXpCapsTimestamps(int hours)
        {
            using (var context = new PourtideDbContext())
            {
                var dailyXpCaps = context.DailyXpCaps.ToList();

                foreach (var cap in dailyXpCaps)
                {
                    cap.StartTimestamp = cap.StartTimestamp.AddHours(hours);
                    cap.EndTimestamp = cap.EndTimestamp.AddHours(hours);
                }

                context.SaveChanges();
            }
        }

        public DailyXpCap GetCurrentDailyXpCap()
        {
            using (var context = new PourtideDbContext())
            {
                DateTime currentDate = DateTime.UtcNow;

                // Retrieve the current DailyXpCap based on the current date
                var currentDailyXpCap = context.DailyXpCaps
                    .FirstOrDefault(d => d.StartTimestamp <= currentDate && d.EndTimestamp >= currentDate);

                return currentDailyXpCap;
            }
        }
    }
}
