using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomGenerator.Scripts.Generators
{

    public class ItemGeneratorBuilder : IGeneratorBuilder
    {
        private readonly Random m_random;
        private readonly HashSet<string> m_strings;

        public ItemGeneratorBuilder(Random random = null, IEnumerable<string> examples = null)
        {
            m_random = random ?? new Random();
            m_strings = examples == null ? new HashSet<string>() : new HashSet<string>(examples);
        }

        public void Teach(IEnumerable<string> examples) 
        {
            if (examples != null)
            {
                foreach (var example in examples)
                    m_strings.Add(example);
            }
        }

        public void Teach(string example)
        {
            if (example != null)
                m_strings.Add(example);
        }


        public IGenerator ToGenerator()
        {
            return new ItemGenerator(m_random, m_strings.ToArray());
        }

        
        private class ItemGenerator : IGenerator
        {
            private readonly Random m_random;
            private readonly string[] m_strings;

            public ItemGenerator(Random random, string[] strings = null)
            {
                m_random = random;
                m_strings = strings;
            }

            public string Generate()
            {
                if (m_strings == null)
                    return null;

                var num = m_random.Next(0, m_strings.Length);
                return m_strings[num];
            }
        }
    }
}