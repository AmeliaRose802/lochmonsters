using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomGenerator.Scripts.Generators
{
    public class MarkovGeneratorBuilder : IGeneratorBuilder
    {
        private readonly Random m_random;
        private readonly int m_order;
        private readonly Dictionary<string, int> m_startingStrings = new Dictionary<string, int>();
        private readonly Dictionary<string, Dictionary<char, int>> m_productions = new Dictionary<string, Dictionary<char, int>>();

        private int m_startingSum;

        public MarkovGeneratorBuilder(int order, Random random = null, IEnumerable<string> examples = null)
        {
            m_order = order;
            m_random = random ?? new Random();

            if (examples != null)
            {
                foreach (var example in examples)
                    Teach(example);
            }
        }

        public void Teach(IEnumerable<string> examples)
        {
            if (examples != null)
            {
                foreach (var example in examples)
                    Teach(example);
            }
        }

        public void Teach(string example)
        {
            example = example.ToLowerInvariant();

            // if the example is shorter than the order, just add a production that this example instantly leads to null
            if (example.Length <= m_order)
            {
                m_startingStrings.AddOrUpdate(example, 1, a => a + 1);
                m_startingSum++;
                Increment(example, '\0');

                return;
            }


            // Chomp string into "order" length parts, and the single letter which follows it
            int lastIndex = example.Length - m_order;
            for (int i = 0; i < lastIndex + 1; i++)
            {
                var key = example.Substring(i, m_order);
                if (i == 0)
                {
                    m_startingStrings.AddOrUpdate(key, 1, a => a + 1);
                    m_startingSum++;
                }

                var sub = i == lastIndex ? '\0' : example[i + m_order];
                Increment(key, sub);
            }
        }

        public IGenerator ToGenerator()
        {
            return new MarkovGenerator(
                m_random,
                m_order,
                NormalizeStartingString(),
                m_productions.ToDictionary(a => a.Key, a => Normalize(a.Value))
            );
        }

        private static KeyValuePair<char, float>[] Normalize(Dictionary<char, int> stringCounts)
        {
            int total = 0;
            foreach (var a in stringCounts)
                total += a.Value;

            return stringCounts.Select(a => new KeyValuePair<char, float>(a.Key, (float)a.Value / total)).ToArray();
        }

        private KeyValuePair<string, float>[] NormalizeStartingString()
        {
            return m_startingStrings.Select(a => new KeyValuePair<string, float>(a.Key, (float)a.Value / m_startingSum)).ToArray();
        }

        private void Increment(string key, char value)
        {
            Dictionary<char, int> set;
            if (!m_productions.TryGetValue(key, out set))
            {
                set = new Dictionary<char, int>(27);
                m_productions.Add(key, set);
            }

            set.AddOrUpdate(value, 1, a => a + 1);
        }


        private class MarkovGenerator : IGenerator
        {
            private readonly Random m_random;
            private readonly int m_order;
            private readonly KeyValuePair<string, float>[] m_startingStrings;
            private readonly Dictionary<string, KeyValuePair<char, float>[]> m_productions;

            public MarkovGenerator(Random random, int order, KeyValuePair<string, float>[] startingStrings, Dictionary<string, KeyValuePair<char, float>[]> productions)
            {
                m_random = random;
                m_order = order;
                m_startingStrings = startingStrings;
                m_productions = productions;
            }

            public string Generate()
            {
                string builder = "";

                string lastSelected = WeightedRandom(m_startingStrings);

                do
                {
                    //Extend string
                    builder += lastSelected;
                    if (builder.Length < m_order)
                        break;

                    //Key to use to find next production
                    var key = builder.Substring(builder.Length - m_order);

                    //Find production rules for this key
                    KeyValuePair<char, float>[] prod;
                    if (!m_productions.TryGetValue(key, out prod))
                        break;

                    //Produce next expansion
                    lastSelected = WeightedRandom(prod).ToString();

                } while (lastSelected != string.Empty &&
                        lastSelected != "\0");

                return builder;
            }

            private T WeightedRandom<T>(KeyValuePair<T, float>[] items)
            {
                var num = m_random.NextDouble();

                for (int i = 0; i < items.Length; i++)
                {
                    var keyValuePair = items[i];
                    num -= keyValuePair.Value;
                    if (num <= 0)
                        return keyValuePair.Key;
                }

                throw new InvalidOperationException();
            }
        }
    }
}