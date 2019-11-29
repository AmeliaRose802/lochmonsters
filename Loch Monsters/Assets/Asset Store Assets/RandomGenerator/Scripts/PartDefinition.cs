using RandomGenerator.Scripts.Generators;
using System.Collections.Generic;
using System.Linq;

namespace RandomGenerator.Scripts
{
    public class PartDefinition
    {
        public Dictionary<string, string[]> Source { get; protected set; }
        public Mode Mode { get; protected set; }
        public int? MaxLength { get; protected set; }
        public MarkovMode? MarkovMode { get; protected set; }
        public int? MarkovOrder { get; protected set; }

        private readonly Dictionary<string, IGenerator> m_generators = new Dictionary<string, IGenerator>();

        public PartDefinition(Dictionary<string, string[]> source, Mode mode, int? maxLength = 10, MarkovMode? markovMode = Scripts.MarkovMode.Word, int? markovOrder = 2)
        {
            Source = source;
            Mode = mode;
            MaxLength = maxLength;
            MarkovMode = markovMode;
            MarkovOrder = markovOrder;
        }

        public string GeneratePart(System.Random random, string type = null)
        {
            var availableTypes = Source.Keys.ToList();
            if (type == null || !availableTypes.Contains(type))
            {
                type = availableTypes[random.Next(availableTypes.Count)];
            }

            var selectedMode = Mode;
            if (selectedMode == Mode.MarkovOrItem)
            {
                selectedMode = (Mode)random.Next(2);
            }

            IGenerator generator;
            if (!m_generators.TryGetValue(selectedMode + type, out generator))
            {
                IGeneratorBuilder generatorBuilder = null;
                switch (selectedMode)
                {
                    case Mode.Item:
                        {
                            generatorBuilder = new ItemGeneratorBuilder(random);
                            break;
                        }
                    case Mode.Markov:
                        {
                            generatorBuilder = new MarkovGeneratorBuilder(MarkovOrder ?? 2, random);
                            break;
                        }
                }

                generatorBuilder.Teach(Source[type]);
                generator = generatorBuilder.ToGenerator();
                m_generators[selectedMode + type] = generator;
            }

            switch (selectedMode)
            {
                case Mode.Item:
                    {
                        return generator.Generate();
                    }
                case Mode.Markov:
                    {
                        string result = null;

                        // Try 10 times for a reasonable length.
                        for (var x = 0; x < 10; x++)
                        {
                            result = generator.Generate();
                            if (result.Length < (MaxLength ?? 10) && result.Length > 2)
                            {
                                break;
                            }
                        }

                        return result;
                    }
            }
            return null;
        }
    }
}