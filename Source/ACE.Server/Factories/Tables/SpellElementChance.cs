using ACE.Common;
using ACE.Entity.Enum;
using System;

namespace ACE.Server.Factories.Tables
{
    public enum Element
    {
        Slash = 0x1,
        Pierce = 0x2,
        Bludgeon = 0x4,
        Cold = 0x8,
        Fire = 0x10,
        Acid = 0x20,
        Electric = 0x40,
    };

    public static class SpellElementChanceExtensions
    {
        public static string GetName(this Element damageType)
        {
            switch (damageType)
            {
                case Element.Slash: return "Slashing";
                case Element.Pierce: return "Piercing";
                case Element.Bludgeon: return "Bludgeoning";
                case Element.Cold: return "Cold";
                case Element.Fire: return "Fire";
                case Element.Acid: return "Acid";
                case Element.Electric: return "Electric";
                default:
                    return null;
            }
        }
    }

    public static class SpellElementChance
    {
        public static Element GetElement()
        {
            var values = System.Enum.GetValues(typeof(Element));
            return (Element)values.GetValue(ThreadSafeRandom.Next(0, values.Length - 1));
        }
    }
}
