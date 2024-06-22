using ACE.Adapter.GDLE.Models;
using ACE.Server.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ACE.Server.Features.Spells
{

    public class Spell
    {
        public uint Id { get; }
        public string Name { get; }
        public Spell(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    internal static class SpellsRepository
    {
        public readonly static Dictionary<uint, Spell> Spells = new Dictionary<uint, Spell>();

        public readonly static Dictionary<uint, Spell> SpellsByName = new Dictionary<uint, Spell>();

        private static string CsvFile = "spells.csv";

        public static void Initialize()
        {
            if (Spells.Count == 0)
            {
                ImporSpellsFromCsv();
            }
        }

        private static void ImporSpellsFromCsv()
        {
            string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Features", "Spells", "spells.csv");

            if (!File.Exists(csvFilePath))
            {
                throw new Exception("Failed to read spells.csv");
            }

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    string id = parts[0];
                    string name = parts[1];
                    uint parsedId;

                    if (uint.TryParse(id, out parsedId))
                        Spells[parsedId] = new Spell(parsedId, name);
                }
            }
        }
    }
}
