using ACE.Common;
using ACE.Entity.Enum;
using System.Collections.Generic;

namespace ACE.Server.Factories.Tables
{
    public static class SlayerChance
    {
        private static readonly List<CreatureType> CreatureTypes = new List<CreatureType>()
        {
            CreatureType.Anekshay,
            CreatureType.Armoredillo,
            CreatureType.Banderling,
            CreatureType.BlightedMoarsman,
            CreatureType.Burun,
            CreatureType.Crystal,
            CreatureType.Drudge,
            CreatureType.Eater,
            CreatureType.Elemental,
            CreatureType.Fiun,
            CreatureType.GearKnight,
            CreatureType.Ghost,
            CreatureType.Golem,
            CreatureType.Gurog,
            CreatureType.Gromnie,
            CreatureType.Human,
            CreatureType.Knathtead,
            CreatureType.Lugian,
            CreatureType.Mattekar,
            CreatureType.Mite,
            CreatureType.Moarsman,
            CreatureType.Mosswart,
            CreatureType.Mukkir,
            CreatureType.Olthoi,
            CreatureType.ParadoxOlthoi,
            CreatureType.Penguin,
            CreatureType.PhyntosWasp,
            CreatureType.Rat,
            CreatureType.Reedshark,
            CreatureType.Remoran,
            CreatureType.Sclavus,
            CreatureType.Shadow,
            CreatureType.Shreth,
            CreatureType.Skeleton,
            CreatureType.Tumerok,
            CreatureType.Tusker,
            CreatureType.Undead,
            CreatureType.Virindi,
            CreatureType.Wisp,
            CreatureType.Zefir
        };

        public static CreatureType GetCreatureType()
        {
           return CreatureTypes[ThreadSafeRandom.Next(0, CreatureTypes.Count - 1)];
        }
    }
}

