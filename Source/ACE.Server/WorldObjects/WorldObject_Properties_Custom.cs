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

        public double ProcSpellChainRate
        {
            get => GetProperty(PropertyFloat.ProcSpellChainRate) ?? 0d;
            set { if (value == 0d) RemoveProperty(PropertyFloat.ProcSpellChainRate); else SetProperty(PropertyFloat.ProcSpellChainRate, value); }
        }

        public double ProcSlowRate
        {
            get => GetProperty(PropertyFloat.ProcSlowRate) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyFloat.ProcSlowRate); else SetProperty(PropertyFloat.ProcSlowRate, value); }
        }
        public double ProcRootRate
        {
            get => GetProperty(PropertyFloat.ProcRootRate) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyFloat.ProcRootRate); else SetProperty(PropertyFloat.ProcRootRate, value); }
        }

        public double ReflectiveDamageMod
        {
            get => GetProperty(PropertyFloat.ReflectiveDamageMod) ?? 0d;
            set { if (value == 0d) RemoveProperty(PropertyFloat.ReflectiveDamageMod); else SetProperty(PropertyFloat.ReflectiveDamageMod, value); }
        }
    }
}
