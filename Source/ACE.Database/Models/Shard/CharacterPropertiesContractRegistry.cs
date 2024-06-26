using System;
using System.Collections.Generic;

#nullable disable

namespace ACE.Database.Models.Shard
{
    public partial class CharacterPropertiesContractRegistry
    {
        public ulong CharacterId { get; set; }
        public uint ContractId { get; set; }
        public bool DeleteContract { get; set; }
        public bool SetAsDisplayContract { get; set; }

        public virtual Character Character { get; set; }
    }
}
