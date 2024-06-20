using ACE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Features.Spells.Managers
{
    public static class SpellsManager
    {
        public static Dictionary<string, SpellId> SpellIdDictionary = new Dictionary<string, SpellId>(StringComparer.OrdinalIgnoreCase);
        public static string GetSpellName(uint spellId)
        {
            if (SpellsRepository.Spells.TryGetValue(spellId, out var name))
                return name;
            else
                return "";
        }

        public static void Initialize()
        {
            SpellsRepository.Initialize();

            foreach (SpellId spellId in Enum.GetValues(typeof(SpellId)))
            {
                SpellIdDictionary[spellId.ToString()] = spellId;
            }
        }

        public static bool TryGetEnumValue(string value, out SpellId spellId)
        {
            return SpellIdDictionary.TryGetValue(value, out spellId);
        }
    }
}
