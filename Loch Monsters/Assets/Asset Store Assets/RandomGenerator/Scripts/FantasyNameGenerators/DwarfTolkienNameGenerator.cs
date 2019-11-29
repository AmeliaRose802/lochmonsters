using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public class DwarfTolkienNameGenerator : Definition
    {
        public DwarfTolkienNameGenerator()
        {
            Name = "Dwarf (Tolkien)";
            SupportedTypes = new[] { Gender.Neutral.ToString() };
            DefinitionFormats = new[]
            {
                new DefinitionFormat(
                    name: "Forename Surname",
                    formats: new[]
                    {
                        new Tuple<string, IEnumerable<PartDefinition>>("{0} {1}{2}", new[]
                        {
                            s_forename,
                            DwarfNames.PrefixAdjective,
                            DwarfNames.SuffixNoun
                        }),
                        new Tuple<string, IEnumerable<PartDefinition>>("{0} {1}{2}", new[]
                        {
                            s_forename,
                            DwarfNames.PrefixNoun,
                            DwarfNames.SuffixAdjective
                        })
                    }
                )
            };
        }

        private static readonly PartDefinition s_forename = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.MarkovOrItem,
            maxLength: 12,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "dagrik", "thaugnit", "utror", "vagnok", "ikekut ", "radivis", "donik", "krazimir", "darbek", "balkom", "egrak",
                        "balin", "bifur", "bofur", "bombur", "dori", "dwalin", "fili", "gloin", "kili", "nori", "oin", "ori", "durin", "fundin",
                        "nain", "thror", "thrain", "farin", "dis", "gimli", "borin", "frerin", "fror", "gror", "frar", "nali", "nar", "narvi",
                        "mim", "ibun", "khim", "akor", "nurot", "kravis", "drazimir", "kanarr", "kadarr", "kromdrak", "beldek", "murmiir"
                    }
                }
            }
        );
    }
}