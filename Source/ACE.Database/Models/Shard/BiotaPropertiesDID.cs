using System;
using System.Collections.Generic;

#nullable disable

namespace ACE.Database.Models.Shard
{
    public partial class BiotaPropertiesDID
    {
        public ulong ObjectId { get; set; }
        public ushort Type { get; set; }
        public uint Value { get; set; }

        public virtual Biota Object { get; set; }
    }
}
