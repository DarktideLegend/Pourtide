using System;

namespace ACE.Database.Models.Pourtide // Adjust namespace as per your project structure
{
    public class PKStatsKill
    {
        public uint PKKillsId { get; set; }
        public ulong KillerId { get; set; }
        public ulong VictimId { get; set; }
        public ushort HomeRealmId { get; set; }
        public ushort CurrentRealmId { get; set; }
        public DateTime EventTime { get; set; }
    }
}
