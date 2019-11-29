using System.Collections.Generic;

namespace RandomGenerator.Scripts
{
    public class DefinitionFormat
    {
        public string Name { get; protected set; }
        public Tuple<string, IEnumerable<PartDefinition>>[] Formats { get; protected set; }

        public DefinitionFormat(string name, Tuple<string, IEnumerable<PartDefinition>>[] formats)
        {
            Name = name;
            Formats = formats;
        }
    }
}