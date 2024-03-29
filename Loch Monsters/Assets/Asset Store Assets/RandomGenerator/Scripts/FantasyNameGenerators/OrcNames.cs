﻿using System.Collections.Generic;

namespace RandomGenerator.Scripts.FantasyNameGenerators
{
    public static class OrcNames
    {
        public static readonly PartDefinition Forename = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.MarkovOrItem,
            source: new Dictionary<string, string[]>(2)
            {
                {
                    "Male", new[]
                    {
                        "abzag", "abzrolg", "abzug", "agganor", "aghurz", "agnar", "agrakh", "agrobal", "agronak", "agstarg", "ahzug",
                        "arghragdush", "arghur", "ashzu", "aturgh", "azarg", "azimbul", "azuk", "bagamul", "bakh", "balagog", "balarkh",
                        "balknakh", "balmeg", "baloth", "balrook", "balzag", "bargrug", "baronk", "bashag", "bashagorn", "bashnag", "bat",
                        "batgrul", "bazgulub", "bazrag", "bazur", "begnar", "bekhwug", "bhagrun", "biknuk", "bisquelas", "blodrat", "bogakh",
                        "boggeryk", "bogham", "bognash", "bogodug", "bogrum", "bogzul", "bolg", "bolgrul", "bologra", "borab", "borbuz", "borgath",
                        "borgh", "borkul", "bormolg", "borolg", "borth", "borug", "borz", "borzighu", "borzugh", "both", "braadoth", "brag",
                        "braghul", "brog", "brogdul", "brokil", "brugagikh", "brugdush", "brugo", "brulak", "bugak", "bugdul", "bugharz", "bugnerg",
                        "bugrash", "bugrol", "bugunh", "bulg", "bullig", "bulozog", "bulugbek", "bulzog", "bumbub", "bumnog", "buragrub", "buramog",
                        "burguk", "burul", "burz", "burzgrag", "burzunguk", "burzura", "buzog", "carzog", "charlvain", "cognor", "dagnub",
                        "dorzogg", "dromash", "dubok", "dugakh", "dugan", "dugroth", "dugtosh", "dugug", "dugugikh", "dul", "dul", "dular", "dular",
                        "dulfish", "dulph", "dulphago", "dulphumph", "dulrat", "duluk", "duma", "dumag", "dumbuk", "dumburz", "dumolg", "dur",
                        "durak", "durak", "durbul", "durgash", "durz", "durzol", "durzub", "durzum", "dushgor", "dushkul", "dushnamub", "dushugg",
                        "fangoz", "farbalg", "fheg", "gadba", "gahgdar", "gahznar", "gard", "gargak", "garmeg", "garnikh", "garothmuk", "garzonk",
                        "gashdug", "gasheg", "gashna", "gat", "gaturn", "gezdak", "gezorz", "ghak", "ghaknag", "ghamborz", "ghamonk", "ghamorz",
                        "ghamron", "ghamulg", "ghashur", "ghatrugh", "ghaturn", "ghaz", "ghobargh", "ghogurz", "ghola", "ghoragdush", "ghorbash",
                        "ghorlorz", "ghornag", "ghornugag", "ghorub", "ghrategg", "ghromrash", "ghunzul", "gladba", "glag", "glagbor", "glamalg",
                        "glaz", "glazgor", "glazulg", "glegokh", "gloorag", "gloorot", "glorgzorgo", "gloth", "glothozug", "glud", "glundeg",
                        "glunrum", "glunurgakh", "glurdag", "glurnt", "glush", "glushonkh", "gluthob", "gluthush", "gobur", "goburbak", "godrun",
                        "godrun", "gogaz", "gogbag", "gogrikh", "gogron", "goh", "gohazgu", "golbag", "golg", "goorgul", "gorak", "goramalg",
                        "gorbakh", "gorblad", "gorbu", "gordag", "gorgath", "gorgo", "gorgrolg", "gorlar", "gorotho", "gorrath", "gorzesh",
                        "gothurg", "gozarth", "graalug", "graklak", "gralturg", "graman", "grashbag", "grashub", "grat", "gravik", "grezgor",
                        "grishduf", "grodagur", "grodoguz", "grogmar", "grommok", "gronov", "grookh", "grubdosh", "grudogub", "grugnur", "grul",
                        "grulbash", "gruldum", "gruloq", "gruluk", "grulzul", "grumth", "grunyun", "grushbub", "grushnag", "grushnag", "gruudus",
                        "gruzdash", "gruzgob", "gruznak", "guarg", "gudrig", "gul", "gulargh", "gularzob", "gulburz", "gulug", "gulzog", "gunagud",
                        "gurak", "gurg", "gurgozod", "gurlak", "guruzug", "gushagub", "guzg", "gwilherm", "hagard", "hanz", "ilthag", "inazzur",
                        "joun", "kargnuth", "kazok", "kelrog", "kentosh", "khadba", "khagra", "khal", "khamagash", "kharag", "khargol", "kharsh",
                        "kharsthun", "khoruzoth", "khralek", "kirgut", "klang", "klovag", "koffutto", "kogaz", "kradauk", "krog", "krognak",
                        "krogrash", "kulth", "kurd", "kurdan", "kurlash", "kurog", "kurog", "kurz", "lagarg", "lagrog", "lahkgarg", "lakhalg",
                        "larak", "largakh", "larhoth", "larob", "lashbag", "latumph", "laurig", "lazgel", "lob", "lob", "logbur", "loghorz",
                        "logogru", "logrun", "lorbash", "lorbumol", "lorzub", "lothdush", "lothgud", "lozotusk", "lozruth", "lug", "lugbagg",
                        "lugbur", "lugdakh", "lugdugul", "lugdum", "lugnikh", "lugolg", "lugrots", "lugrub", "lugrun", "lugzod", "lum", "lum",
                        "lumdum", "lumgol", "lungruk", "lunk", "lurash", "lurbozog", "lurbuk", "lurg", "lurgonash", "lurog", "luronk", "luzmash",
                        "maaga", "mag", "magra", "magub", "magunh", "mahk", "makhel", "makhoguz", "makhug", "maknok", "makor", "malkus", "marzul",
                        "mash", "matuk", "maugash", "mauhul", "mauhulakh", "mazabakh", "mazgro", "mazogug", "mazorn", "mekag", "mog", "mog",
                        "mogazgur", "mogrub", "mogrul", "mokhrul", "mokhul", "mol", "morbash", "mordrog", "mordugul", "mordularg", "morgaz",
                        "morgbrath", "morlak", "morothmash", "morotub", "mort", "moth", "mothozog", "muduk", "mudush", "mug", "mug", "mugdul",
                        "muglugd", "muhaimin", "muk", "mul", "mulatub", "mulgargh", "mulgu", "mulush", "murag", "murdodosh", "murgoz", "murgrud",
                        "murkh", "murkub", "murlog", "murzog", "murzol", "muzbar", "muzdrulz", "muzgalg", "muzgash", "muzgonk", "muzgu", "muzogu",
                        "nag", "nagoth", "nagrub", "nagrul", "nahzgra", "nahzush", "namoroth", "nar", "narazz", "nargbagorn", "narhag",
                        "narkhagikh", "narkhozikh", "narkhukulg", "narkularz", "nash", "nash", "nenesh", "norgol", "nugok", "nugwugg", "nunkuk",
                        "obdeg", "obgol", "obgurob", "obrash", "ofglog", "oglub", "ogmash", "ogog", "ogol", "ogorosh", "ogozod", "ogruk", "ogrul",
                        "ogrumbu", "ogularz", "ogumalg", "ogzor", "okrat", "olfim", "olfin", "olfin", "olgol", "olpacatulg", "olugush", "olumba",
                        "olur", "ontogu", "oodeg", "oodegu", "oorg", "oorgurn", "oorlug", "orakh", "ordooth", "orgak", "orgdugrash", "orgotash",
                        "orntosh", "orok", "orzbara", "orzuk", "osgrikh", "osgulug", "othbug", "othigu", "othmash", "othogor", "othohoth",
                        "otholug", "othukul", "othulg", "othzog", "ozor", "pergol", "rablarz", "ragbul", "ragbur", "ragnast", "ramazbur", "ramolg",
                        "ramorgol", "ramosh", "razgor", "razgugul", "rhosh", "rogbum", "rogdul", "rognar", "rogrug", "rogthun,", "rogurog",
                        "rokaug", "rokut", "roog", "rooglag", "rorburz", "rozag", "rugdugbash", "rugdumph", "rugmeg", "ruzgrol", "sgolag", "shab",
                        "shagol", "shagol", "shagrod", "shagrol", "shakh", "shakh", "shakhighu", "shamar", "shamar", "shamlakh", "shamob",
                        "shargam", "shargarkh", "sharkagub", "sharkub", "sharkuzog", "sharnag", "shat", "shazgob", "shelakh", "shobob", "shogorn",
                        "shugral", "shukul", "shulong", "shulthog", "shum", "shura", "shurkol", "shurkul", "shuzug", "skagurn", "skagwar",
                        "skalgunh", "skalguth", "skarath", "skordo", "skulzak", "slagwug", "slayag", "slegbash", "smagbogoth", "smauk", "snagbash",
                        "snagg", "snagh", "snaglak", "snakh", "snakha", "snakzut", "snalikh", "snarbugag", "snargorg", "snat", "snazumph",
                        "snegbug", "snegburgak", "snegh", "snikhbat", "snoog", "snoorg", "snugar", "snugok", "snukh", "snushbat", "sogh", "spagel",
                        "storgh", "stugbrulz", "szugburg", "szugogroth", "targoth", "tazgol", "tazgul", "thagbush", "thakh", "thakush", "tharag",
                        "tharkul", "thaz", "thazeg", "thaznog", "thegur", "tholog", "thorzh", "thorzhul", "thrag", "thragdosh", "thragosh", "threg",
                        "thrug", "thrugb", "thukbug", "thungdosh", "todrak", "togbrig", "tograz", "torg", "torug", "tugawuz", "tumuthag", "tungthu",
                        "ufthag", "ugdumph", "ugdush", "uggnath", "ughash", "ugorz", "ugruntuk", "uguntig", "ugurz", "ulag", "ulagash", "ulagug",
                        "ulam", "ulang", "ulgdagorn", "ulghesh", "ulmug", "umug", "umurn", "undrigug", "undugar", "ungruk", "unrahg", "unthrikh",
                        "urag", "uragor", "urak", "uram", "urbul", "urdbug", "urgdosh", "urim", "urmuk", "urok", "urul", "urul", "urzog", "ushamph",
                        "ushang", "ushat", "ushnar", "usn", "usnagikh", "uthik", "uulgarg", "uuth", "uzgakhbashnag", "uznom", "uzul", "vargos",
                        "waghuth", "wardush", "wort", "yadba", "yagak", "yagorkh", "yagramak", "yak", "yakegg", "yam", "yamarz", "yambagorn",
                        "yambul", "yar", "yargob", "yargol", "yarnag", "yashnarz", "yat", "yatur", "yggnast", "yggoz", "yggruk", "yzzgol", "zagh",
                        "zaghurbak", "zagrakh", "zagrugh", "zbulg", "zegol", "zgog", "zhagush", "zhosh", "zilbash", "zogbag", "zulgozu", "zungarg",
                        "zunlogdurgob"
                    }
                },
                {
                    "Female", new[]
                    {
                        "abimfash", "adkul", "adlugbuk", "agazu", "agdesh", "aglash", "agli", "agrash", "agrob", "agrulla", "agzurz", "akash",
                        "akgruhl", "akkra", "aklash", "alga", "arakh", "argulla", "argurgol", "arob", "arzakh", "arzorag", "ashaka", "ashgara",
                        "ashgel", "ashrashag", "atoga", "atorag", "atub", "atugol", "aza", "azabesh", "azadhai", "azhnakha", "azhnolga", "azhnura",
                        "azilkh", "azlakha", "azulga", "baag", "baagug", "badbog", "badush", "bafthaka", "baggi", "bagrak", "bagrugbesh", "bagul",
                        "bagula", "barazal", "barza", "bashuk", "batara", "batasha", "batorabesh", "batul", "batum", "bazbava", "bazgara",
                        "bhagruan", "bluga", "bogdub", "bolar", "bolash", "bolugbeka", "bor", "borba", "borbgur", "borbuga", "borgakh",
                        "borgburakh", "borgdorga", "borgrara", "boroth", "borzog", "bugbekh", "bugbesh", "bugdurash", "bugha", "bula", "bulak",
                        "bularkh", "bulfim", "bulfor", "bum", "bumava", "bumbuk", "bumph", "bumzuna", "burub", "burzob", "dagarha", "drienne",
                        "droka", "druga", "dufbash", "dulasha", "dulfra", "dulfraga", "dulkhi", "dulroi", "dulug", "dumoga", "dumuguk", "dumurzog",
                        "dura", "duragma", "durga", "durgat", "durgura", "durhaz", "durida", "durogbesh", "durz", "dushug", "emen", "engong",
                        "erisa", "fnagdesh", "gahgra", "garakh", "gargum", "garl", "garlor", "garlub", "garotusha", "gashnakh", "ghak", "ghamzeh",
                        "gharakul", "gharn", "gharol", "ghat", "gheshol", "ghob", "ghogogg", "ghorza", "ghorzolga", "ghratutha", "glagag",
                        "glagosh", "glarikha", "glash", "glasha", "glath", "glathut", "glesh", "glob", "glolbikla", "glothum", "glurbasha",
                        "glurduk", "glurmghal", "gluronk", "glurzul", "gluth", "gluthesh", "gnush", "gogul", "golga", "gondubaga", "gonk", "goorga",
                        "grabash", "graghesh", "grahla", "grahuar", "graklha", "grash", "grashla", "grashug", "grat", "grazda", "grazob",
                        "grazubesha", "greme", "grenbet", "groddi", "gronir", "grubalash", "grubathag", "grugleg", "grumgha", "grundag", "gruzbura",
                        "guazh", "gul", "gula", "gulara", "gulfim", "gulgula", "gulorz", "gulugash", "gulza", "gurhul", "gursthuk", "gurum", "guth",
                        "guurzash", "guuth", "guz", "guzash", "guzmara", "haghai", "harza", "homraz", "hurabesh", "ilg", "irsugha", "jorthan",
                        "kansif", "karel", "kashurthag", "katusi", "ketasha", "ketuzi", "khagral", "khagruk", "khaguga", "khaguur", "kharzug",
                        "khazrakh", "kora", "korgha", "kroma", "kruaga", "kuhlon", "kurz", "lagabul", "lagakh", "laganakh", "lagbaal", "lagbuga",
                        "lagezura", "lagra", "lagruda", "lahzga", "lakhazga", "lamazh", "lambug", "lambur", "lamburak", "lamugbek", "lamur",
                        "larzgug", "lash", "lashakh", "lashbesh", "lashbura", "lashdura", "lashgikh", "lashgurgol", "lashza", "lazdutha", "lazgar",
                        "lazgara", "lazghal", "legdul", "lig", "logdotha", "loglorag", "logru", "lokra", "lorak", "lorogdu", "luga", "lugharz",
                        "luglorash", "lugrugha", "lurgush", "luruzesh", "lurz", "mabgrorga", "mabgrubaga", "mabgruhl", "magula", "maraka",
                        "marutha", "maugruhl", "mazgar", "mazgroth", "mazoga", "mazrah", "mazuka", "megruk", "merani", "mog", "mogak", "mogdurz",
                        "moglurkgul", "mograg", "mogul", "mor", "mordra", "morga", "morn", "mornamph", "morndolag", "mozgosh", "mugaga",
                        "mugumurn", "muguur", "multa", "mulzah", "mulzara", "murbul", "murob", "murotha", "murzgut", "murzush", "muzgraga", "myev",
                        "nargol", "narzdush", "nazdura", "nazhag", "nazhataga", "nazubesh", "neega", "noguza", "nunchak", "nuza", "oghash", "ogzaz",
                        "oorga", "oorza", "oorzuka", "orag", "orbuhl", "orbul", "orcolag", "ordatha", "orgotha", "orlozag", "orlugash", "orluguk",
                        "orthuna", "orutha", "orzbara", "orzdara", "orzorga", "oshgana", "othbekha", "othgozag", "othikha", "othrika", "ovak",
                        "ownka", "ozrog", "pruzag", "pruzga", "ragash", "ragbarlag", "ragushna", "rahami", "rakhaz", "rakuga", "ranarsh", "rasashi",
                        "razbela", "rogag", "rogba", "rogbut", "rogmesh", "rogoga", "rogzesh", "roku", "rolfikha", "rolfish", "rolga", "rulbagab",
                        "rulbza", "ruldor", "rulfala", "rulfim", "rulfub", "rulfuna", "sgala", "sgrugbesh", "sgrugha", "sgrula", "shabaga",
                        "shabeg", "shabeshga", "shabgrut", "shabon", "shadbak", "shagar", "shagareg", "shagdub", "shagduka", "shagora", "shagrum",
                        "shagrush", "shagura", "shakul", "shalug", "shamuk", "shamush", "shara", "sharamph", "sharbzur", "sharduka", "shardush",
                        "shardzozag", "shargra", "sharn", "sharog", "sharuk", "sharushnam", "shautha", "shaza", "shebakh", "shel", "sheluka",
                        "shelur", "sholg", "shubesha", "shufdal", "shufgrut", "shufthakul", "shuftharz", "shuga", "shugzar", "shurkul", "shuzrag",
                        "sloogolga", "sloomalah", "sluz", "snagara", "snak", "snarataga", "snarga", "snargara", "snaruga", "sneehash", "snilga",
                        "snoogh", "snushbesh", "solgra", "sorook", "stilga", "stroda", "stuga", "stughrush", "sumane", "sutha", "temati", "theg",
                        "thegbesh", "thegshakul", "thegshalash", "theshaga", "theshgoth", "thishnaku", "thogra", "thrugrak", "thugnekh", "thulga",
                        "thushleg", "tugha", "ubzigub", "ufalga", "ufgabesh", "ufgaz", "ufgel", "ufgra", "uftheg", "ugak", "ugarnesh", "ugduk",
                        "ugor", "ugrash", "ugrush", "uldushna", "ulg", "ulgush", "uloth", "ulsha", "ulu", "ulubesh", "uluga", "ulukhaz", "ulumpha",
                        "umar", "umbugbek", "umgubesh", "umog", "umutha", "umzolabesh", "undorga", "undush", "undusha", "uratag", "urbzag",
                        "urdboga", "urgarlag", "urog", "uruka", "urzoga", "urzoth", "urzul", "urzula", "usha", "ushaga", "ushenat", "ushruka",
                        "ushug", "ushuta", "uzka", "vola", "volen", "voltha", "vosh", "vumnish", "vush", "yag", "yakhu", "yarlak", "yarulorz",
                        "yatanakh", "yatul", "yatular", "yatzog", "yazara", "yazgash", "yazgruga", "yazoga", "yevelda", "zaag", "zagla", "zagula",
                        "zenenir", "zubesha", "zugh", "zuniner", "zuugarz", "zuuthag", "zuuthusha"
                    }
                }
            }
        );

        public static readonly PartDefinition SurnamePrefix = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(2)
            {
                {
                    "Male", new[]
                    {
                        "gro"
                    }
                },
                {
                    "Female", new[]
                    {
                        "gra"
                    }
            }
            }
        );

        public static readonly PartDefinition Surname = new PartDefinition
        (
            markovOrder: 2,
            mode: Mode.Item,
            source: new Dictionary<string, string[]>(1)
            {
                {
                    "Neutral", new[]
                    {
                        "agadbu", "agamph", "agdur", "aglakh", "agum", "arzug", "atumph", "azorku", "badbu", "badbul", "bagol", "bagrat", "bagul",
                        "bamog", "bar", "bar", "barak", "barbol", "bargamph", "bargol", "bashnag", "bashnarz", "bat", "batul", "bazgar", "bekh",
                        "bharg", "birgo", "boga", "bogamakh", "bogharz", "bogla", "boglar", "bogrol", "boguk", "bol", "bol", "bolak", "bolmog",
                        "bonk", "bor", "borbog", "borbul", "borgakh", "borgub", "born", "brok", "bug", "bugarn", "buglump", "bugurz", "bulag",
                        "bularz", "bulfimorn", "bulfish", "bumolg", "bumph", "bura", "burbog", "burbug", "burbulg", "burgal", "burish", "burku",
                        "burol", "burz", "burzag", "buzbee", "buzga", "cromgog", "dasik", "dragol", "drol", "drom", "drublog", "dugronk", "dugul",
                        "dul", "dula", "dulak", "dulob", "duluk", "dumul", "dumulg", "durbug", "durga", "durgamph", "durgoth", "durog", "durug",
                        "dush", "dushnikh", "dushnikh", "fakal", "galash", "gamorn", "gar", "garbug", "gash", "gashel", "gat", "gatuk", "gatuk",
                        "ghammak", "gharz", "ghash", "ghasharzol", "ghol", "gholfim", "gholob", "ghorak", "ghoth", "ghralog", "glorzuf", "gluk",
                        "glurkub", "glurzog", "goldfolly", "golpok", "gonk", "gortwog", "gorzog", "gorzoth", "grambak", "grulam", "gular", "gulfim",
                        "gum", "gurakh", "gurba", "gurub", "guthra", "hubrag", "kashug", "khagdum", "khamagash", "khambol", "khamug", "khar",
                        "kharbush", "khargub", "kharz", "khash", "khash", "khashnar", "khatub", "khazgur", "khazor", "korith", "korma", "kruts",
                        "kush", "ladba", "lag", "lagdub", "largum", "lazgarn", "logdum", "loghash", "logob", "logrob", "lorga", "lort", "lumborn",
                        "lumbuk", "lumob", "lurkul", "lurn", "luruk", "luzgan", "madba", "magar", "magrish", "magul", "makla", "malog", "malorz",
                        "mar", "marguz", "marob", "mashnar", "mashul", "mogduk", "moghakh", "morad", "morgrump", "morkul", "mughol", "muk",
                        "mulakh", "murgak", "murgob", "murgol", "murkha", "murug", "murug", "murz", "muzgob", "muzgol", "muzgub", "muzgur", "namor",
                        "nar", "narzul", "naybek", "nogremor", "nolob", "ogar", "ogdub", "ogdum", "oglurn", "olor", "olub", "oluk", "olurba",
                        "orbuma", "orgak", "orguk", "orkul", "orkulg", "othmog", "ram", "rimat", "rimph", "rugdush", "rugob", "ruguk", "rush",
                        "rushub", "ruumsh", "sgrugdul", "shadborgob", "shadbuk", "shagdub", "shagdulg", "shagk", "shagob", "shagrak", "shagrak",
                        "shagramph", "shagronk", "shak", "sham", "shamub", "shar", "sharbag", "sharga", "shargakh", "sharob", "sharob", "sharolg",
                        "shat", "shatub", "shatub", "shatur", "shatur", "shazgul", "shazog", "shelakh", "shelob", "sheluk", "shub", "shub", "shug",
                        "shug", "shugarz", "shugduk", "shugdurbam", "shugham", "shugharz", "shuhgharz", "shula", "shulor", "shumba", "shura",
                        "shurkul", "shurkul", "shuzgub", "skandar", "snagarz", "snagdu", "stugbaz", "stugh", "thormok", "ufthamph", "uftharz",
                        "ugdub", "ugruma", "ugrush", "ular", "ulfimph", "urgak", "urgash", "urku", "urkub", "urula", "ushar", "usharku", "ushug",
                        "ushul", "uzgash", "uzguk", "uzgurn", "uzug", "uzuk", "vortag", "wroggin", "yagarz", "yak", "yargul", "yarug", "yarzol",
                        "yarzol", "yggrub"
                    }
                }
            }
        );
    }
}