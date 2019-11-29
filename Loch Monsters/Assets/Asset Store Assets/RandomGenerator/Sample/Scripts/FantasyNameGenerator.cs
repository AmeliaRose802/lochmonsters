using RandomGenerator.Scripts;
using RandomGenerator.Scripts.FantasyNameGenerators;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RandomGenerator.Sample.Scripts
{
    public class FantasyNameGenerator : MonoBehaviour
    {
        private System.Random m_random;
        private readonly Dictionary<string, Definition> m_definitions = new Dictionary<string, Definition>(11)
        {
            { "Dwarf (Tolkien)", new DwarfTolkienNameGenerator() },
            { "Elf", new ElfNameGenerator() },
            { "Human", new HumanNameGenerator()},
            { "Orc", new OrcNameGenerator()},
        };

        public Text text;

        void Start()
        {
            m_random = new System.Random();
        }

        public void GenerateName(string generator)
        {
            Definition definition;
            if (m_definitions.TryGetValue(generator, out definition))
            {
                text.text = generator + ": " + definition.Generate(m_random);
            }
        }
    }
}