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

        public static Dictionary<string, uint> SpellIdDictionaryByReadableName = new Dictionary<string, uint>();

        public static bool GetSpellIdFromReadableName(string name, out uint id)
        {
            if (SpellIdDictionaryByReadableName.TryGetValue(name, out var spellId))
            {
                id = spellId;
                return true;
            }
            else
            {
                id = 0;
                return false;
            }
        }

        public static string GetSpellName(uint spellId)
        {
            if (SpellsRepository.Spells.TryGetValue(spellId, out var spell))
                return spell.Name;
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

            foreach(var spell in SpellsRepository.Spells.Values.ToList())
            {
                SpellIdDictionaryByReadableName[spell.Name] = spell.Id;
            }
        }

        public static bool TryGetEnumValue(string value, out SpellId spellId)
        {
            return SpellIdDictionary.TryGetValue(value, out spellId);
        }
    }
}
