using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public class HumanNameGenerator : Definition
    {
        public HumanNameGenerator()
        {
            Name = "Human";
            SupportedTypes = new[] { Gender.Neutral.ToString() };
            DefinitionFormats = new[]
            {
                new DefinitionFormat(
                    name: "Forename Surname",
                    formats: new[]
                    {
                        new Tuple<string, IEnumerable<PartDefinition>>("{0} {1}{2}", new[]
                        {
                            HumanNames.Forename,
                            HumanNames.SurnamePrefix,
                            HumanNames.SurnameSuffix
                        })
                    }
                )
            };
        }
    }
}