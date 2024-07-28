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
        public double ProcBurnRate
        {
            get => GetProperty(PropertyFloat.ProcBurnRate) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyFloat.ProcBurnRate); else SetProperty(PropertyFloat.ProcBurnRate, value); }
        }

        public double ReflectiveDamageMod
        {
            get => GetProperty(PropertyFloat.ReflectiveDamageMod) ?? 0d;
            set { if (value == 0d) RemoveProperty(PropertyFloat.ReflectiveDamageMod); else SetProperty(PropertyFloat.ReflectiveDamageMod, value); }
        }

        public int? BountyGuid
        {
            get => GetProperty(PropertyInt.BountyGuid);
            set { if (value == null) RemoveProperty(PropertyInt.BountyGuid); else SetProperty(PropertyInt.BountyGuid, value.Value); }
        }

        public int? BountyTrophyGuid
        {
            get => GetProperty(PropertyInt.BountyTrophyGuid);
            set { if (value == null) RemoveProperty(PropertyInt.BountyTrophyGuid); else SetProperty(PropertyInt.BountyTrophyGuid, value.Value); }
        }

        public int ForgottenOreTier
        {
            get => GetProperty(PropertyInt.ForgottenOreTier) ?? 4;
            set { if (value == 0) RemoveProperty(PropertyInt.ForgottenOreTier); else SetProperty(PropertyInt.ForgottenOreTier, value); }
        }

        public int? BountyCreationTimeStamp
        {
            get => GetProperty(PropertyInt.BountyCreationTimestamp);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.BountyCreationTimestamp); else SetProperty(PropertyInt.BountyCreationTimestamp, value.Value); }
        }

        public int PlayerBountyTrackingCount
        {
            get => GetProperty(PropertyInt.PlayerBountyTrackingCount) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyInt.PlayerBountyTrackingCount); else SetProperty(PropertyInt.PlayerBountyTrackingCount, value); }
        }
    }
}
