using System;
using System.Collections.Generic;

#nullable disable

namespace ACE.Database.Models.Pourtide
{
    public class XpCap
    {
        public uint Id { get; set; }
        public DateTime DailyTimestamp { get; set; }
        public DateTime WeeklyTimestamp { get; set; }
        public uint Week { get; set; }
    }
}
