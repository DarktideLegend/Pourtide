using System;
using ACE.Entity.Enum;

namespace ACE.Server.Network.GameEvent.Events
{
    public class GameEventAttackerNotification : GameEventMessage
    {
        public GameEventAttackerNotification(ISession session, string defenderName, DamageType damageType, float percent, uint damage, bool criticalHit, AttackConditions attackConditions)
            : base(GameEventType.AttackerNotification, GameMessageGroup.UIQueue, session, 76) // 76 is the max seen in retail pcaps
        {
            Writer.WriteString16L(defenderName);
            Writer.Write((uint)damageType);
            Writer.Write((double)percent);
            Writer.Write(damage);
            Writer.Write(Convert.ToUInt32(criticalHit));
            Writer.WriteNonGuidULong((ulong)attackConditions);
        }
    }
}
