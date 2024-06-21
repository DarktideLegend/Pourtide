using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.WorldObjects
{
    // Add custom properties here
    partial class WorldObject
    {
        public bool IsMorphGem
        {
            get => GetProperty(PropertyBool.IsMorphGem) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.IsMorphGem); else SetProperty(PropertyBool.IsMorphGem, value); }
        }

        public double SpellChainChance
        {
            get => GetProperty(PropertyFloat.SpellChainChance) ?? 0d;
            set { if (value == 0d) RemoveProperty(PropertyFloat.SpellChainChance); else SetProperty(PropertyFloat.SpellChainChance, value); }
        }


    }
}
