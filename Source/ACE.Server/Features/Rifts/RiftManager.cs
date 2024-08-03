using ACE.Common;
using ACE.Database;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Features.HotDungeons.Managers;
using ACE.Server.Managers;
using ACE.Server.Realms;
using ACE.Server.WorldObjects;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace ACE.Server.Features.Rifts
{
    internal static class RiftManager
    {
        private static readonly object lockObject = new object();

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public readonly static Position RiftIslandMaxPosition = InstancedPosition.slocToPosition("0x370A0025 [101.407288 106.575951 0.005000] -0.690305 0.000000 0.000000 0.723518");

        public readonly static Position RiftIslandMinPosition = InstancedPosition.slocToPosition("0x420b003d [182.259476 107.198425 -0.445000] -0.932283 0.000000 0.000000 -0.361731");

        public readonly static double MaxIslandDistance = RiftIslandMaxPosition.Distance2D(RiftIslandMinPosition);

        public readonly static double Tier1IslandThreshold = MaxIslandDistance * 0.75;

        public readonly static double Tier2IslandThreshold = MaxIslandDistance * 0.5;

        public readonly static List<ushort> RiftIslandRealmIds = new List<ushort>() { 300 };

        public static Dictionary<ushort, Dictionary<string, Rift>> ActiveRifts = new Dictionary<ushort, Dictionary<string, Rift>>();

        public static Dictionary<uint, Rift> ActiveRiftsByInstance = new Dictionary<uint, Rift>();

        public static Dictionary<ushort, Dictionary<ushort, List<uint>>> RiftMonsterTable = new Dictionary<ushort, Dictionary<ushort, List<uint>>>()
        {
            {
                1, new Dictionary<ushort, List<uint>>
                {
                    { 1, new List<uint> {
                        22519, 22520, 231, 32768, 24304, 2482
                    } },
                    { 2, new List<uint> {

                        24299, 1610, 7089, 7090, 7105
                    } },
                    { 3, new List<uint> {
                        2483, 29345, 28644, 28643
                    } },
                }
            }
        };

        public static void RemoveRiftPlayer(string lb, Player player)
        {
            var guid = player.Guid.Full;

            if (ActiveRifts.ContainsKey(player.HomeRealm))
            {
                if (ActiveRifts[player.HomeRealm].TryGetValue(lb, out Rift rift))
                {
                    if (rift.Players.ContainsKey(guid))
                    {
                        rift.Players.Remove(guid);
                        log.Info($"Removed {player.Name} from {rift.Name}");
                    }
                }
            }
        }

        public static void AddRiftPlayer(string nextLb, Player player)
        {
            var guid = player.Guid.Full;

            if (ActiveRifts.ContainsKey(player.HomeRealm))
            {
                if (ActiveRifts[player.HomeRealm].TryGetValue(nextLb, out Rift rift))
                {
                    if (!rift.Players.ContainsKey(guid))
                    {
                        rift.Players.TryAdd(guid, player);
                        log.Info($"Added {player.Name} to {rift.Name}");
                    }
                }
            }
        }

        public static bool HasActiveRift(ushort realmId, string lb)
        {
            if (ActiveRifts.ContainsKey(realmId))
                return ActiveRifts[realmId].ContainsKey(lb);

            return false;
        }

        public static List<Rift> GetRifts()
        {
            return ActiveRifts.Values.SelectMany(kvp => kvp.Values).ToList();
        }

        public static bool TryGetActiveRift(ushort realmId, string lb, out Rift activeRift)
        {
            if (ActiveRifts.ContainsKey(realmId))
            {
                if (ActiveRifts[realmId].TryGetValue(lb, out activeRift))
                {
                    return true;
                }
                else
                {
                    activeRift = null;
                    return false;
                }
            }

            activeRift = null;
            return false;
        }

        public static bool TryGetActiveRift(uint instance, out Rift activeRift)
        {
            if (ActiveRiftsByInstance.ContainsKey(instance))
            {
                activeRift = ActiveRiftsByInstance[instance];
                return true;
            }

            activeRift = null;
            return false;
        }

        internal static WorldObject ProcessRiftIslandObject(WorldObject wo, AppliedRuleset ruleset)
        {
            switch (wo.WeenieType)
            {
                case WeenieType.Creature:
                    return ProcessRiftIslandCreature(wo, ruleset);
                case WeenieType.Generic:
                    return ProcessRiftGenerator(wo, ruleset);
                default:
                    return null;
            }
        }

        private static WorldObject ProcessRiftGenerator(WorldObject wo, AppliedRuleset ruleset)
        {
            var creatureRespawnDuration = ruleset.GetProperty(RealmPropertyFloat.CreatureRespawnDuration);
            var creatureSpawnRateMultiplier = ruleset.GetProperty(RealmPropertyFloat.CreatureSpawnRateMultiplier);

            if (creatureRespawnDuration > 0)
            {
                wo.RegenerationInterval = (int)((float)creatureRespawnDuration * creatureSpawnRateMultiplier);

                wo.ReinitializeHeartbeats();

                if (wo.Biota.PropertiesGenerator != null)
                {
                    // While this may be ugly, it's done for performance reasons.
                    // Common weenie properties are not cloned into the bota on creation. Instead, the biota references simply point to the weenie collections.
                    // The problem here is that we want to update one of those common collection properties. If the biota is referencing the weenie collection,
                    // then we'll end up updating the global weenie (from the cache), instead of just this specific biota.
                    if (wo.Biota.PropertiesGenerator == wo.Weenie.PropertiesGenerator)
                    {
                        wo.Biota.PropertiesGenerator = new List<ACE.Entity.Models.PropertiesGenerator>(wo.Weenie.PropertiesGenerator.Count);

                        foreach (var record in wo.Weenie.PropertiesGenerator)
                            wo.Biota.PropertiesGenerator.Add(record.Clone());
                    }
                }
            }

            return wo;
        }

        private static WorldObject ProcessRiftIslandCreature(WorldObject wo, AppliedRuleset ruleset)
        {
            WorldObject creature = null;

            // tier one island
            if (ruleset.Realm.Id == 300 && !wo.IsGenerator)
            {
                var creatureTable = RiftMonsterTable[1];
                var distanceToEnd = wo.Location.Distance2D(new LocalPosition(RiftIslandMaxPosition));
                ushort tableTier;

                if (distanceToEnd >= Tier1IslandThreshold)
                    tableTier = 1;
                else if (distanceToEnd >= Tier2IslandThreshold)
                    tableTier = 2;
                else
                    tableTier = 3;

                if (tableTier > 0)
                {
                    var creatureList = creatureTable[tableTier];
                    var wcid = creatureList[ThreadSafeRandom.Next(0, creatureList.Count - 1)];
                    creature = WorldObjectFactory.CreateNewWorldObject(wcid);
                    creature.Location = new InstancedPosition(wo.Location);
                }
            }

            wo.Destroy();

            return creature;
        }

        /*public static List<WorldObject> GetDungeonObjectsFromPosition(InstancedPosition position)
        {
            var Id = new LandblockId(position.LandblockId.Raw);

            var objects = DatabaseManager.World.GetCachedInstancesByLandblock(Id.Landblock);
            return objects.Select(link => WorldObjectFactory.CreateNewWorldObject(link.WeenieClassId)).ToList();
        }

        private static List<Creature> GetGeneratorCreaturesObjectsFromDungeon(List<WorldObject> dungeonObjects)
        {
            var objects = dungeonObjects
                .Where(wo => wo.IsGenerator)
                .SelectMany(wo => wo.Biota.PropertiesGenerator.Select(prop => prop.WeenieClassId))
                .Select(wcid => WorldObjectFactory.CreateNewWorldObject(wcid))
                .OfType<Creature>()
                .Where(creature => creature is not Player && !creature.IsGenerator && !creature.IsNPC && creature.DeathTreasure != null)
                .ToList();

            return objects;
        }

        public static (uint, List<uint>) GetRiftCreatureIds(InstancedPosition dropPosition)
        {
            var dungeonObjects = GetDungeonObjectsFromPosition(dropPosition);

            var generatorCreatureObjects = GetGeneratorCreaturesObjectsFromDungeon(dungeonObjects);

            var spawnedCreatures = dungeonObjects
                .OfType<Creature>()
                .Where(creature => creature is not Player && !creature.IsGenerator && !creature.IsNPC && creature.DeathTreasure != null);

            var creatures = generatorCreatureObjects.Concat(spawnedCreatures).Distinct().ToList();

            var groupedCreaturesByLootTier = creatures.GroupBy(c => c.DeathTreasure.Tier).Select(g => new { Tier = g.Key, Count = g.Count() });
            var groupedCreaturesByLevel = creatures.GroupBy(c => c.Level).Select(g => new { Level = g.Key, Count = g.Count() });

            var mostCommonLoot = groupedCreaturesByLootTier.OrderByDescending(l => l.Count).Select(l => l.Tier).FirstOrDefault();
            var mostCommonLevel = groupedCreaturesByLevel.OrderByDescending(l => l.Count).Select(l => l.Level).FirstOrDefault();

            if (mostCommonLoot == 0)
                mostCommonLoot = 1;

            if (mostCommonLevel == null || mostCommonLevel == 0)
                mostCommonLevel = 1;

            var tier = MutationsManager.GetMonsterTierByLevel((uint)mostCommonLevel);

            var creatureWeenieIds = DatabaseManager.World.GetDungeonCreatureWeenieIds(mostCommonLoot);

            var creatureIds = creatureWeenieIds
                .Where(c => c.Level >= mostCommonLevel && c.Level <= mostCommonLevel + 20)
                .Select(c => c.Id)
                .ToList();

            creatures.ForEach(c => c.Destroy()); ;
            creatures.Clear();

            return (tier, creatureIds);
        }

        public static Rift CreateRiftInstance(ushort realmId, Dungeon dungeon)
        {
            var rules = new List<Realm>()
            {
                RealmManager.GetRealm(1016, includeRulesets: true).Realm // rift ruleset
            };
            var ephemeralRealm = RealmManager.GetNewEphemeralLandblock(realmId, dungeon.DropPosition.LandblockId, rules, true);

            var instance = ephemeralRealm.Instance;

            var dropPosition = new InstancedPosition(dungeon.DropPosition, instance);

            var (tier, creatureIds) = GetRiftCreatureIds(dropPosition);

            var rift = new Rift(dungeon.Landblock, dungeon.Name, dungeon.Coords, dropPosition, instance, dungeon.DropPosition.Instance, ephemeralRealm, creatureIds, tier);

            log.Info($"Creating Rift instance for {rift.Name} - {instance}");

            return rift;
        }

        private static List<WorldObject> FindRandomCreatures(Rift rift)
        {
            var lb = rift.LandblockInstance;

            var worldObjects = lb.GetAllWorldObjectsForDiagnostics();

            var filteredWorldObjects = worldObjects
                .Where(wo => wo is Creature creature && !(creature is Player) && !wo.IsGenerator)
                .OrderBy(creature => creature, new DistanceComparer(rift.DropPosition))
                .ToList(); 

            return filteredWorldObjects;
        }

        private class DistanceComparer : IComparer<WorldObject>
        {
            private InstancedPosition Location;
            public DistanceComparer(InstancedPosition location)
            {
                Location = location;
            }
            public int Compare(WorldObject x, WorldObject y)
            {
                return (int)(x.Location.DistanceTo(Location) - y.Location.DistanceTo(Location));
            }
        }

        internal static bool TryAddRift(ushort realmId, string currentLb, Dungeon dungeon, out Rift addedRift)
        {
            addedRift = null;

            if (!ActiveRifts.ContainsKey(realmId))
                ActiveRifts.TryAdd(realmId, new Dictionary<string, Rift>());


            if (ActiveRifts[realmId].ContainsKey(currentLb))
                return false;

            var rift = CreateRiftInstance(realmId, dungeon);

            SpawnHomeToRiftPortalAsync(rift);
            SpawnRiftToHomePortalAsync(rift);

            var rifts = ActiveRifts[realmId].Values.ToList();

            var last = rifts.LastOrDefault();

            if (last != null)
            {
                rift.Previous = last;
                last.Next = rift;

                SpawnNextLinkAsync(last);
                SpawnPreviousLinkAsync(rift);
            }

            ActiveRifts[realmId][currentLb] = rift;
            ActiveRiftsByInstance[rift.Instance] = rift;

            addedRift = rift;

            var at = rift.Coords.Length > 0 ? $"at {rift.Coords}" : "";
            var message = $"Dungeon {rift.Name} {at} is now an activated Rift";
            log.Info(message);

            return true;
        }

        internal static void SpawnRiftToHomePortalAsync(Rift rift)
        {
            if (rift.DropPosition == null)
                return;

            var landblock = rift.LandblockInstance;
            var chain = new ActionChain();
            chain.AddDelaySeconds(5);

            chain.AddAction(landblock, () =>
            {

                if (!landblock.CreateWorldObjectsCompleted)
                {
                    SpawnRiftToHomePortalAsync(rift);
                    return;
                }

                List<WorldObject> creatures;

                creatures = FindRandomCreatures(rift);

                foreach (var wo in creatures.Skip(3).ToList())
                {
                    Portal portal = (Portal)WorldObjectFactory.CreateNewWorldObject(600004);
                    portal.Name = $"Dungeon Portal {rift.Name}";
                    portal.Location = new InstancedPosition(wo.Location);
                    portal.Destination = new InstancedPosition(wo.Location, wo.Location.Instance).AsLocalPosition();
                    portal.Lifespan = int.MaxValue;

                    var name = "Portal to " + rift.Name;
                    portal.SetProperty(ACE.Entity.Enum.Properties.PropertyString.AppraisalPortalDestination, name);
                    portal.ObjScale *= 0.25f;

                    log.Info($"Attempting to link dungeon in rift {rift.Name}");
                    if (portal.EnterWorld())
                    {
                        log.Info($"Added dungeon portal for rift {rift.Name}");
                        return;
                    }
                }
            });
            chain.EnqueueChain();
        }


        public static void SpawnHomeToRiftPortalAsync(Rift rift)
        {
            if (rift.DropPosition == null)
                return;

            var landblock = LandblockManager.GetLandblock(rift.LandblockInstance.Id, rift.HomeInstance, null, false);
            var chain = new ActionChain();
            chain.AddDelaySeconds(5);

            chain.AddAction(landblock, () =>
            {

                var toRiftDrop = new InstancedPosition(rift.DropPosition, rift.HomeInstance);

                Portal portal = (Portal)WorldObjectFactory.CreateNewWorldObject(600004);
                portal.Name = $"Rift Portal {rift.Name}";
                portal.Location = new InstancedPosition(toRiftDrop);

                var dest = new InstancedPosition(rift.DropPosition, rift.Instance);

                portal.Destination = new InstancedPosition(dest).AsLocalPosition();
                portal.IsEphemeralRealmPortal = true;
                portal.EphemeralRealmPortalInstanceID = rift.Instance;
                portal.Lifespan = int.MaxValue;

                var name = "Portal to " + rift.Name;
                portal.SetProperty(ACE.Entity.Enum.Properties.PropertyString.AppraisalPortalDestination, name);
                portal.ObjScale *= 0.25f;

                rift.RiftPortals.Add(portal);

                portal.EnterWorld();
            });
            chain.EnqueueChain();
        }

        internal static void SpawnPreviousLinkAsync(Rift rift)
        {
            if (rift.DropPosition == null)
                return;

            var landblock = rift.LandblockInstance;
            var chain = new ActionChain();
            chain.AddDelaySeconds(5);

            chain.AddAction(landblock, () =>
            {

                if (!landblock.CreateWorldObjectsCompleted)
                {
                    SpawnPreviousLinkAsync(rift);
                    return;
                }

                List<WorldObject> creatures;

                creatures = FindRandomCreatures(rift);

                if (rift.Previous != null)
                {
                    log.Info($"Creatures Count {creatures.Count} in {rift.Name}");

                    foreach (var wo in creatures)
                    {
                        Portal portal = (Portal)WorldObjectFactory.CreateNewWorldObject(600004);
                        portal.Name = $"Rift Portal {rift.Previous.Name}";
                        portal.Location = new InstancedPosition(wo.Location);
                        portal.Destination = rift.Previous.DropPosition.AsLocalPosition();
                        portal.IsEphemeralRealmPortal = true;
                        portal.EphemeralRealmPortalInstanceID = rift.Previous.Instance;
                        portal.Lifespan = int.MaxValue;

                        var name = "Portal to " + rift.Previous.Name;
                        portal.SetProperty(ACE.Entity.Enum.Properties.PropertyString.AppraisalPortalDestination, name);
                        portal.ObjScale *= 0.25f;

                        log.Info($"Attempting to link Portal for previous in {rift.Name}");
                        if (portal.EnterWorld())
                        {
                            log.Info($"Added Linked Portal for previous in {rift.Name}");
                            return;
                        }
                    }
                }
            });
            chain.EnqueueChain();
        }

        internal static void SpawnNextLinkAsync(Rift rift)
        {
            if (rift.DropPosition == null)
                return;

            var landblock = rift.LandblockInstance;
            var chain = new ActionChain();
            chain.AddDelaySeconds(5);

            chain.AddAction(landblock, () =>
            {
                if (!landblock.CreateWorldObjectsCompleted)
                {
                    SpawnNextLinkAsync(rift);
                    return;
                }

                List<WorldObject> creatures;

                creatures = FindRandomCreatures(rift);

                if (rift.Next != null)
                {
                    log.Info($"Creatures Count {creatures.Count} in {rift.Name}");
                    foreach (var wo in creatures)
                    {
                        Portal portal = (Portal)WorldObjectFactory.CreateNewWorldObject(600004);
                        portal.Name = $"Rift Portal {rift.Next.Name}";
                        portal.Location = new InstancedPosition(wo.Location);
                        portal.Destination = rift.Next.DropPosition.AsLocalPosition();
                        portal.IsEphemeralRealmPortal = true;
                        portal.EphemeralRealmPortalInstanceID = rift.Next.Instance;
                        portal.Lifespan = int.MaxValue;

                        var name = "Portal to " + rift.Next.Name;
                        portal.SetProperty(ACE.Entity.Enum.Properties.PropertyString.AppraisalPortalDestination, name);
                        portal.ObjScale *= 0.25f;

                        log.Info($"Attempting to link Portal for next in {rift.Name}");
                        if (portal.EnterWorld())
                        {
                            log.Info($"Added Linked Portal for next in {rift.Name}");
                            return;
                        }
                    }
                }
            });

            chain.EnqueueChain();
        }
        */
    }
}
