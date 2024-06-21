using ACE.Entity.Enum;
using ACE.Server.Physics;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.WorldObjects
{
    public static class Player_Extensions
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<PhysicsObj> GetVisibleCreaturesByDistance(this Player origin) => origin.GetVisibleCreaturesByDistance(origin);

        public static List<PhysicsObj> GetVisibleCreaturesByDistance(this Player reference, WorldObject origin)
        {
            var visible = reference.PhysicsObj.ObjMaint.GetVisibleObjectsValuesWhere(o =>
                o.WeenieObj.WorldObject.WeenieType == WeenieType.Creature &&    //Restrict to creature weenies here for speed?
                o.WeenieObj.WorldObject != null);

            visible.Sort((x, y) => origin.Location.SquaredDistanceTo(x.WeenieObj.WorldObject.Location)
                        .CompareTo(origin.Location.SquaredDistanceTo(y.WeenieObj.WorldObject.Location)));

            return visible;
        }

        public static List<Creature> GetSplashTargets(this Player reference, WorldObject origin, int maxTargets = 3, float maxRange = 5.0f)
        {
            if (origin is null || reference is null)
            {
                log.Warn($"Failed to get splash targets.");
                return new List<Creature>();
            }

            //List<PhysicsObj> visible;
            //// sort visible objects by ascending distance
            //if (origin is Player)
            //    visible = (origin as Player).GetVisibleCreaturesByDistance();

            //else visible = null;
            //origin.GetVisibleCreaturesByDistance();
            var visible = reference.GetVisibleCreaturesByDistance(origin);

            var splashTargets = new List<Creature>();

            foreach (var obj in visible)
            {
                //Pplashing skips original target?
                if (obj.ID == origin.PhysicsObj.ID)
                    continue;

                //Only splash creatures?
                var creature = obj.WeenieObj.WorldObject as Creature;
                if (creature == null || creature.Teleporting || creature.IsDead) continue;

                if (creature is Player playerObject && reference.CheckPKStatusVsTarget(playerObject, null) != null && playerObject.IsAlly(reference.HomeRealm, reference)) continue;

                //if (player != null && player.CheckPKStatusVsTarget(creature, null) != null)
                //    continue;

                if (!creature.Attackable && creature.TargetingTactic == TargetingTactic.None || creature.Teleporting)
                    continue;

                //if (creature is CombatPet && (player != null || this is CombatPet))
                //    continue;

                //No objects in range
                var cylDist = origin.GetCylinderDistance(creature);
                if (cylDist > maxRange)
                    return splashTargets;

                //Filter by angle?
                //var angle = creature.GetAngle(origin);
                // if (Math.Abs(angle) > splashAngle / 2.0f)
                //     continue;

                //Found splash object
                splashTargets.Add(creature);

                //Stop if you've found enough targets
                if (splashTargets.Count == maxTargets)
                    break;
            }
            return splashTargets;
        }
    }
}
