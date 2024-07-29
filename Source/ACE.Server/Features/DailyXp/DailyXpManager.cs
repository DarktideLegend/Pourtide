using ACE.Database;
using ACE.Common;
using ACE.DatLoader.FileTypes;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Managers;
using ACE.Server.Network.GameAction.Actions;
using ACE.Server.WorldObjects;
using log4net;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Features.DailyXp
{
    internal class DailyXpManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class DailyXp(DateTime endTimeStamp, DateTime startTimeStamp, uint week, uint day, ulong xpCap)
        {
            public readonly DateTime EndTimeStamp = endTimeStamp;
            public readonly DateTime StartTimestamp = startTimeStamp;
            public readonly uint Day = day;
            public readonly uint Week = week;
            public readonly ulong XpCap = xpCap;
        }

        public static DateTime DailyTimestamp { get; private set; }

        public static DailyXp CurrentDailyXp { get; private set; }

        public static uint Week => CurrentDailyXp.Week;
        public static uint Day => CurrentDailyXp.Day;

        private static bool IsDailyTimestampExpired => DailyTimestamp < DateTime.UtcNow;

        private static bool Initialized = false;

        public static readonly Dictionary<uint, ulong> WeeklyLevelWithCapXp = new Dictionary<uint, ulong>()
        {
            { 1, 38335275 },
            { 2, 150013037 },
            { 3, 387419625 },
            { 4, 859755734 },
            { 5, 1709581309 },
            { 6, 3128116563 },
            { 7, 5362412965 },
            { 8, 8722524219 },
            { 9, 13588677261 },
            { 10, 20418443236 },
            { 11, 29753908491 },
            { 12, 42228845559 },
            { 13, 58575884147 },
            { 14, 79633682122 },
            { 15, 106354096497 },
            { 16, 191226310247 }
        };

        private static Dictionary<(uint, uint), ulong> DistributeWeeklyXp(Dictionary<uint, ulong> weeklyXp)
        {
            var dailyXp = new Dictionary<(uint, uint), ulong>();
            ulong previousWeekXp = 0; // Starting XP

            foreach (var week in weeklyXp.Keys.OrderBy(w => w))
            {
                ulong maxXp = weeklyXp[week];
                const uint totalDays = 7;

                ulong totalXpToDistribute = maxXp - previousWeekXp;

                ulong baseXp = totalXpToDistribute / totalDays;

                for (uint day = 1; day <= totalDays; day++)
                {
                    ulong xpForDay = baseXp + previousWeekXp;

                    if (day == totalDays)
                        xpForDay = maxXp;

                    dailyXp[(week, day)] = xpForDay;

                    previousWeekXp = xpForDay;
                }
            }

            return dailyXp;
        }

        public static void ResetDailyXpCap()
        {
            var dailyXpData = DistributeWeeklyXp(WeeklyLevelWithCapXp);
            DatabaseManager.Pourtide.ResetDailyXpCaps(dailyXpData);
        }

        private static DailyXp FetchDailyXpCap()
        {
            var currentDailyXpCap = DatabaseManager.Pourtide.GetCurrentDailyXpCap();

            if (currentDailyXpCap != null)
                return new DailyXp(currentDailyXpCap.EndTimestamp, currentDailyXpCap.StartTimestamp, currentDailyXpCap.Week, currentDailyXpCap.Day, currentDailyXpCap.DailyXp);
            else
                return new DailyXp(DateTime.MaxValue, DateTime.MinValue, 0, 0, 0);
        }

        private static void FetchDailyXp()
        {
            CurrentDailyXp = FetchDailyXpCap();
            DailyTimestamp = CurrentDailyXp.EndTimeStamp;
        }

        public static void Initialize()
        {
            GetMaxLevelPlayer();
            FetchDailyXp();
            Initialized = true;
        }

        public static void Tick()
        {
            if (!Initialized)
            {
                log.Warn("Warning, XpManager was not initialized, daily cap tick will not be processed!");
                return;
            }

            if (IsDailyTimestampExpired)
                FetchDailyXp();
        }

        public static void ResetPlayersForDaily()
        {
            var players = PlayerManager.GetAllPlayers();
            foreach (var player in players)
                SetPlayerXpCap(player);
        }

        public static void ResetPlayersForDaily(string name)
        {
            var player = PlayerManager.FindByName(name);
            if (player != null)
                SetPlayerXpCap(player);
        }

        public static void SetPlayerXpCap(IPlayer player)
        {
            var homeRealm = player.GetProperty(ACE.Entity.Enum.Properties.PropertyInt.HomeRealm);
            if (homeRealm == null)
                return;

            if ((ushort)homeRealm != RealmManager.CurrentSeason.Realm.Id)
                return;

            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.QuestXp, 0);
            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.MonsterXp, 0);
            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.PvpXp, 0);

            var playerTotalXp = player.GetProperty(ACE.Entity.Enum.Properties.PropertyInt64.TotalExperience);
            var dailyMax = (long)CurrentDailyXp.XpCap - (long)playerTotalXp;

            if (dailyMax <= 0)
            {
                player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXpRemaining, 0);
                player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXpMaxPerCategory, 0);
                player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXp, 0);
                return;
            }

            var dailyMaxXpPerCategory = (long)(dailyMax * 0.7);

            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXp, dailyMax);
            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXpRemaining, dailyMax);
            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt64.DailyXpMaxPerCategory, dailyMaxXpPerCategory);
            player.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.DailyXpTimestamp, (int)Time.GetUnixTime(DailyTimestamp));

            player.SaveBiotaToDatabase();
        }

        public static double MaxLevel { get; private set; }

        public static void GetMaxLevelPlayer()
        {
            var maxLevel = PlayerManager.GetAllPlayers()
                .Where(player => {
                    var homeRealm = player.GetProperty(ACE.Entity.Enum.Properties.PropertyInt.HomeRealm);
                    var isPlayer = player.Account.AccessLevel == (uint)AccessLevel.Player;
                    var isCurrentSeason = (ushort)homeRealm == RealmManager.CurrentSeason.Realm.Id;
                    var isDeleted = player.IsPendingDeletion || player.IsDeleted;
                    return homeRealm != null &&
                        isPlayer &&
                        !isDeleted &&
                        isCurrentSeason;
                })
                .OrderByDescending(player => player.Level)
                .Select(player => player.Level)
                .Take(1)
                .FirstOrDefault();

            if (maxLevel == null)
                return;

           MaxLevel = (double)maxLevel;
        }

        public static void UpdateMaxLevel(double level)
        {
            MaxLevel = level;
        }

        public static double GetPlayerLevelXpModifier(int level)
        {
           return (double)MaxLevel / (double)level;
        }
    }
}
