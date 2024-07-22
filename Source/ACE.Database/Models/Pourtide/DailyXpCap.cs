using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Database.Models.Pourtide
{
    public class DailyXpCap
    {
        [Key, Column(Order = 0)]
        public uint Week { get; set; }

        [Key, Column(Order = 1)]
        public uint Day { get; set; }

        public ulong DailyXp { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime EndTimestamp { get; set; }
    }
}
