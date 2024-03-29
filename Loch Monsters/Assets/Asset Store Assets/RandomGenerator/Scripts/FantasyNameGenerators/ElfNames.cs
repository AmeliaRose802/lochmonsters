﻿using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public static class ElfNames
    {
        public static readonly PartDefinition ForenamePrefixConsonant = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "ael", "aer", "af", "ah", "al", "am", "an", "ang", "ansr", "ar", "arn", "bael", "bes", "cael", "cal", "cas", "cor",
                        "cy", "dho", "du", "eil", "eir", "el", "er", "ev", "fir", "fis", "gael", "gar", "gil", "il", "jar", "kan", "ker",
                        "keth", "koeh", "kor", "laf", "lam", "mal", "my", "nai", "nim", "py", "r", "raer", "ren", "rh", "rid", "rum", "seh",
                        "sel", "sim", "sol", "sum", "syl", "t", "tahl", "ther", "uth", "ver", "vil", "von", "zy"
                    }
                }
            }
        );

        public static readonly PartDefinition ForenamePrefixVowel = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "ama", "arì", "aza", "cla", "dae", "dre", "fera", "fi", "ha", "hu", "ia", "ja", "ka", "la", "lue", "ly", "mai", "mara",
                        "na", "nu", "ny", "re", "ru", "rua", "sae", "sha", "she", "si", "ta", "tha", "tho", "thro", "tia", "tra", "ya", "za"
                    }
                }
            }
        );

        public static readonly PartDefinition ForenameSuffixVowel = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "a", "abrar", "adar", "ae", "ael", "aer", "aera", "aethas", "aethus", "afel", "ah", "aha", "ahal", "ahel", "aia",
                        "aias", "aira", "aith", "al", "ala", "ali", "am", "ama", "an", "ana", "ani", "anis", "ar", "ara", "aral", "ari", "aro",
                        "aruil", "as", "asar", "asel", "ash", "ashk", "ath", "ather", "atri", "atria", "atril", "avain", "avar", "avara",
                        "avel", "avia", "avin", "azair", "ean", "eath", "efel", "el", "ela", "ele", "elis", "emar", "en", "er", "erl", "ern",
                        "eruill", "ess", "eth", "etha", "ethal", "ethar", "ethil", "eti", "evar", "ezara", "ia", "ian", "ianna", "iat", "iel",
                        "ihal", "ihar", "ihel", "ii", "ik", "il", "ila", "ilam", "im", "imil", "in", "inal", "inar", "ine", "ion", "ir", "ira",
                        "ire", "is", "isal", "isar", "isel", "iss", "ist", "itae", "itas", "iten", "ith", "ithar", "odar", "ola", "olan", "on",
                        "onal", "onna", "or", "oro", "oth", "othi", "ua", "ual", "uanna", "uath", "udrim", "uhar", "ulam", "umil", "us", "uth",
                        "ya", "yn", "yr", "yth"
                    }
                }
            }
        );

        public static readonly PartDefinition ForenameSuffixConsonant = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "la", "lae", "lam", "lan", "lanna", "lar", "las", "lath", "lean", "lia", "lian", "lie", "lihn", "lirr", "lis", "lith",
                        "llae", "llinn", "lua", "luth", "lyn", "lys", "lyth", "ma", "mah", "mahs", "mil", "mus", "nae", "nal", "nes", "nin",
                        "nine", "nis", "nyn", "que", "quis", "ra", "rad", "rae", "raee", "rah", "rahd", "rail", "ral", "ran", "rath", "re",
                        "reen", "reth", "ri", "ri", "ria", "ria", "ro", "ro", "ron", "ruil", "ryl", "sah", "sal", "sali", "san", "sar", "sel",
                        "sha", "she", "shor", "spar", "tae", "tas", "ten", "thal", "thar", "thas", "ther", "thi", "thil", "thir", "thus", "ti",
                        "til", "tria", "tril", "vain", "van", "vanna", "var", "vara", "via", "vin", "wyn"
                    }
                }
            }
        );

        public static readonly PartDefinition Surname = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.MarkovOrItem,
            maxLength: 12,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "aldanae", "augendil", "auglothir", "augmirial", "celetanas", "cromlond", "cromvathar", "ealodiir", "elervathar",
                        "falaelon", "gaereial", "galadiir", "galaelenil", "galondiir", "haalvaar", "haemin", "haeval", "isilbrinar",
                        "landirthar", "lanlithil", "lassraias", "lithvir", "mataas", "mayamae", "mithantinu", "mithrandir", "mithtanellyn",
                        "nellond", "nelvandal", "ondomiel", "ondorina", "rhuielon", "rhuios", "rhuiviel", "rhuvien", "roloeth", "ruviel",
                        "ruvien", "talindar", "talithdar", "telathion", "tinuren", "tinuviel", "undomiel", "undomin", "undotaur", "vanvathar"
                    }
                }
            }
        );
    }
}