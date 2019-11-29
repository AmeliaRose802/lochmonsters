using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public class OrcNameGenerator : Definition
    {
        public OrcNameGenerator()
        {
            Name = "Orc";
            SupportedTypes = new[] { Gender.Neutral.ToString() };
            DefinitionFormats = new[]
            {
                new DefinitionFormat(
                    name: "Forename Surname",
                    formats: new[]
                    {
                        new Tuple<string, IEnumerable<PartDefinition>>("{0} {1}-{2}", new[]
                        {
                            OrcNames.Forename,
                            OrcNames.SurnamePrefix,
                            OrcNames.Surname
                        })
                    }
                )
            };
        }
    }
}