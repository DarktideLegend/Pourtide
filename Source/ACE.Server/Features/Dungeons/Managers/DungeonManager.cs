using ACE.Adapter.GDLE.Models;
using ACE.Common;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Features.Discord;
using ACE.Server.Features.HotDungeons;
using ACE.Server.Features.Rifts;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Physics.Common;
using ACE.Server.Realms;
using ACE.Server.WorldObjects;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Features.HotDungeons.Managers
{
    public class Dungeon : DungeonBase
    {
        public Dungeon(string landblock, string name, string coords) : base(landblock, name, coords)
        {
            Landblock = landblock;
            Name = name;
            Coords = coords;
        }
    }


    public static class DungeonManager
    {
        private static object dungeonsLock = new object();

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Initialize(uint intialDelay = 30)
        {
            DungeonRepository.Initialize();
        }

        public static bool HasDungeon(string lb)
        {
            return DungeonRepository.HasDungeon(lb);
        }

        public static DungeonLandblock GetDungeonLandblock(string lb)
        {
            return DungeonRepository.ReadonlyLandblocks[lb];
        }

        public static bool HasDungeonLandblock(string lb)
        {
            return DungeonRepository.ReadonlyLandblocks.ContainsKey(lb);
        }

        public static bool TryGetDungeonLandblock(string lb, out DungeonLandblock landblock)
        {
            return DungeonRepository.ReadonlyLandblocks.TryGetValue(lb, out landblock);
        }


        internal static void ProcessCreaturesDeath(string currentLb, Player damager, int xpOverride, out double returnValue)
        {
            returnValue = 1; // Default value

            var realmId = damager.HomeRealm;

            return;

        }
    }

}
