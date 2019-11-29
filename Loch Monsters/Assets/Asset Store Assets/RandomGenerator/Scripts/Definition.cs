using System;
using System.Linq;

namespace RandomGenerator.Scripts
{
    public class Definition
    {
        public string Name { get; protected set; }
        public string[] SupportedTypes { get; protected set; }

        public DefinitionFormat[] DefinitionFormats { get; protected set; }

        public string Generate(Random random, DefinitionFormat definitionFormat = null, string type = null)
        {
            // Choose a name format.
            DefinitionFormat definitionFormatToUse;
            if (definitionFormat != null && Array.IndexOf(DefinitionFormats, definitionFormat) > -1)
            {
                definitionFormatToUse = definitionFormat;
            }
            else
            {
                definitionFormatToUse = DefinitionFormats[random.Next(DefinitionFormats.Length)];
            }

            var format = definitionFormatToUse.Formats[random.Next(definitionFormatToUse.Formats.Length)];
            var parts = format.Item2.Select(part => part.GeneratePart(random, type)).ToArray();
            var result = string.Format(format.Item1, parts);

            return result;
        }
    }
}