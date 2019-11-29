using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public class ElfNameGenerator : Definition
    {
        public ElfNameGenerator()
        {
            Name = "Elf";
            SupportedTypes = new[] { Gender.Neutral.ToString() };
            DefinitionFormats = new[]
            {
                new DefinitionFormat(
                    name: "Forename Surname",
                    formats: new[]
                    {
                        new Tuple<string, IEnumerable<PartDefinition>>("{0}{1} {2}", new[]
                        {
                            ElfNames.ForenamePrefixConsonant,
                            ElfNames.ForenameSuffixVowel,
                            ElfNames.Surname
                        }),
                        new Tuple<string, IEnumerable<PartDefinition>>("{0}{1} {2}", new[]
                        {
                            ElfNames.ForenamePrefixVowel,
                            ElfNames.ForenameSuffixConsonant,
                            ElfNames.Surname
                        })
                    }
                )
            };
        }
    }
}