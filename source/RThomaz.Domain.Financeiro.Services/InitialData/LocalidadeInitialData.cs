using RThomaz.Domain.Financeiro.Entities;
using RThomaz.Infra.CrossCutting.Storage;
using System;
using System.Linq;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Entity;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Infra.Data.Persistence.Contexts;

namespace RThomaz.Domain.Financeiro.Services.InitialData
{
    public class LocalidadeInitialData : IDisposable
    {
        #region private constants        

        private const string BandeiraStorageFolder = "pais/bandeira";

        #endregion
        
        #region private fields

        private readonly RThomazDbContext _context;
        private readonly string _appStorageBucketName;
        private readonly string _paisDirectoryName;
        private readonly StorageHelper _storageHelper;

        #endregion

        #region constructors

        public LocalidadeInitialData()
        {
            _context = new RThomazDbContext();

            _appStorageBucketName = ConfigurationManager.AppSettings["Google.AppStorageBucketName"];

            _storageHelper = new StorageHelper();
            
            string assemblyLocalPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var assemblyDirectoryName = Path.GetDirectoryName(assemblyLocalPath);
            _paisDirectoryName = Path.Combine(assemblyDirectoryName, "InitialData", "Files", "Pais");            
        }

        #endregion

        #region public voids

        public async Task Seed()
        {           
            var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                await SeedPais();
                await SeedEstado();

                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                }
            }
        }

        #endregion

        #region private voids

        private async Task SeedPais()
        {
            await AddOrUpdatePais("Afeganistão", "AF", "AFG", "004", "93", ".af", "af");
            await AddOrUpdatePais("Ilhas Åland", "AX", "ALA", "248", "358", ".ax", "ax");
            await AddOrUpdatePais("Albânia", "AL", "ALB", "008", "355", ".al", "al");
            await AddOrUpdatePais("Argélia", "DZ", "DZA", "012", "213", ".dz", "dz");
            await AddOrUpdatePais("Samoa Americana", "AS", "ASM", "016", "1+684", ".as", "as");
            await AddOrUpdatePais("Andorra", "AD", "AND", "020", "376", ".ad", "ad");
            await AddOrUpdatePais("Angola", "AO", "AGO", "024", "244", ".ao", "ao");
            await AddOrUpdatePais("Anguilla", "AI", "AIA", "660", "1+264", ".ai", "ai");
            await AddOrUpdatePais("Antárctida", "AQ", "ATA", "010", "672", ".aq", null);
            await AddOrUpdatePais("Antigua e Barbuda", "AG", "ATG", "028", "1+268", ".ag", "ag");
            await AddOrUpdatePais("Argentina", "AR", "ARG", "032", "54", ".ar", "ar");
            await AddOrUpdatePais("Arménia", "AM", "ARM", "051", "374", ".am", "am");
            await AddOrUpdatePais("Aruba", "AW", "ABW", "533", "297", ".aw", "aw");
            await AddOrUpdatePais("Austrália", "AU", "AUS", "036", "61", ".au", "au");
            await AddOrUpdatePais("Áustria", "AT", "AUT", "040", "43", ".at", "at");
            await AddOrUpdatePais("Azerbeijão", "AZ", "AZE", "031", "994", ".az", "az");
            await AddOrUpdatePais("Bahamas", "BS", "BHS", "044", "1+242", ".bs", "bs");
            await AddOrUpdatePais("Bahrain", "BH", "BHR", "048", "973", ".bh", "bh");
            await AddOrUpdatePais("Bangladesh", "BD", "BGD", "050", "880", ".bd", "bd");
            await AddOrUpdatePais("Barbados", "BB", "BRB", "052", "1+246", ".bb", "bb");
            await AddOrUpdatePais("Bielo-Rússia", "BY", "BLR", "112", "375", ".by", "by");
            await AddOrUpdatePais("Bélgica", "BE", "BEL", "056", "32", ".be", "be");
            await AddOrUpdatePais("Belize", "BZ", "BLZ", "084", "501", ".bz", "bz");
            await AddOrUpdatePais("Benin", "BJ", "BEN", "204", "229", ".bj", "bj");
            await AddOrUpdatePais("Bermuda", "BM", "BMU", "060", "1+441", ".bm", "bm");
            await AddOrUpdatePais("Butão", "BT", "BTN", "064", "975", ".bt", "bt");
            await AddOrUpdatePais("Bolívia", "BO", "BOL", "068", "591", ".bo", "bo");
            await AddOrUpdatePais("Sint Eustatius e Saba Bonaire", "BQ", "BES", "535", "599", ".bq", null);
            await AddOrUpdatePais("Bósnia-Herzegovina", "BA", "BIH", "070", "387", ".ba", "ba");
            await AddOrUpdatePais("Botswana", "BW", "BWA", "072", "267", ".bw", "bw");
            await AddOrUpdatePais("Ilha Bouvet", "BV", "BVT", "074", null, ".bv", "bv");
            await AddOrUpdatePais("Brasil", "BR", "BRA", "076", "55", ".br", "br");
            await AddOrUpdatePais("Território Britânico do Oceano Índico", "IO", "IOT", "086", "246", ".io", "io");
            await AddOrUpdatePais("Brunei", "BN", "BRN", "096", "673", ".bn", "bn");
            await AddOrUpdatePais("Bulgária", "BG", "BGR", "100", "359", ".bg", "bg");
            await AddOrUpdatePais("Burkina Faso", "BF", "BFA", "854", "226", ".bf", "bf");
            await AddOrUpdatePais("Burundi", "BI", "BDI", "108", "257", ".bi", "bi");
            await AddOrUpdatePais("Cambodja", "KH", "KHM", "116", "855", ".kh", "kh");
            await AddOrUpdatePais("Camarões", "CM", "CMR", "120", "237", ".cm", "cm");
            await AddOrUpdatePais("Canadá", "CA", "CAN", "124", "1", ".ca", "ca");
            await AddOrUpdatePais("Cabo Verde", "CV", "CPV", "132", "238", ".cv", "cv");
            await AddOrUpdatePais("Ilhas Cayman", "KY", "CYM", "136", "1+345", ".ky", "ky");
            await AddOrUpdatePais("República Centro-africana", "CF", "CAF", "140", "236", ".cf", "cf");
            await AddOrUpdatePais("Chade", "TD", "TCD", "148", "235", ".td", "td");
            await AddOrUpdatePais("Chile", "CL", "CHL", "152", "56", ".cl", "cl");
            await AddOrUpdatePais("China", "CN", "CHN", "156", "86", ".cn", "cn");
            await AddOrUpdatePais("Ilha Christmas", "CX", "CXR", "162", "61", ".cx", "cx");
            await AddOrUpdatePais("Ilhas Cocos", "CC", "CCK", "166", "61", ".cc", "cc");
            await AddOrUpdatePais("Colômbia", "CO", "COL", "170", "57", ".co", "co");
            await AddOrUpdatePais("Comores", "KM", "COM", "174", "269", ".km", "km");
            await AddOrUpdatePais("República do Congo", "CG", "COG", "178", "242", ".cg", "cg");
            await AddOrUpdatePais("Ilhas Cook", "CK", "COK", "184", "682", ".ck", "ck");
            await AddOrUpdatePais("Costa Rica", "CR", "CRI", "188", "506", ".cr", "cr");
            await AddOrUpdatePais("Costa do Marfim", "CI", "CIV", "384", "225", ".ci", "ci");
            await AddOrUpdatePais("Croácia", "HR", "HRV", "191", "385", ".hr", "hr");
            await AddOrUpdatePais("Cuba", "CU", "CUB", "192", "53", ".cu", "cu");
            await AddOrUpdatePais("Curacao", "CW", "CUW", "531", "599", ".cw", null);
            await AddOrUpdatePais("Chipre", "CY", "CYP", "196", "357", ".cy", "cy");
            await AddOrUpdatePais("República Checa", "CZ", "CZE", "203", "420", ".cz", "cz");
            await AddOrUpdatePais("República Democrática do (antigo Zaire) Congo", "CD", "COD", "180", "243", ".cd", "cd");
            await AddOrUpdatePais("Dinamarca", "DK", "DNK", "208", "45", ".dk", "dk");
            await AddOrUpdatePais("Djibouti", "DJ", "DJI", "262", "253", ".dj", "dj");
            await AddOrUpdatePais("Dominica", "DM", "DMA", "212", "1+767", ".dm", "dm");
            await AddOrUpdatePais("República Dominicana", "DO", "DOM", "214", "1+809, 8", ".do", "do");
            await AddOrUpdatePais("Equador", "EC", "ECU", "218", "593", ".ec", "ec");
            await AddOrUpdatePais("Egipto", "EG", "EGY", "818", "20", ".eg", "eg");
            await AddOrUpdatePais("El Salvador", "SV", "SLV", "222", "503", ".sv", "sv");
            await AddOrUpdatePais("Guiné Equatorial", "GQ", "GNQ", "226", "240", ".gq", "gq");
            await AddOrUpdatePais("Eritreia", "ER", "ERI", "232", "291", ".er", "er");
            await AddOrUpdatePais("Estónia", "EE", "EST", "233", "372", ".ee", "ee");
            await AddOrUpdatePais("Etiópia", "ET", "ETH", "231", "251", ".et", "et");
            await AddOrUpdatePais("Ilhas Malvinas (Falkland)", "FK", "FLK", "238", "500", ".fk", "fk");
            await AddOrUpdatePais("Ilhas Faroe", "FO", "FRO", "234", "298", ".fo", "fo");
            await AddOrUpdatePais("Fiji", "FJ", "FJI", "242", "679", ".fj", "fj");
            await AddOrUpdatePais("Finlândia", "FI", "FIN", "246", "358", ".fi", "fi");
            await AddOrUpdatePais("França", "FR", "FRA", "250", "33", ".fr", "fr");
            await AddOrUpdatePais("Guiana Francesa", "GF", "GUF", "254", "594", ".gf", "gf");
            await AddOrUpdatePais("Polinésia Francesa", "PF", "PYF", "258", "689", ".pf", "pf");
            await AddOrUpdatePais("Terras Austrais e Antárticas Francesas (TAAF)", "TF", "ATF", "260", null, ".tf", "tf");
            await AddOrUpdatePais("Gabão", "GA", "GAB", "266", "241", ".ga", "ga");
            await AddOrUpdatePais("Gâmbia", "GM", "GMB", "270", "220", ".gm", "gm");
            await AddOrUpdatePais("Geórgia", "GE", "GEO", "268", "995", ".ge", "ge");
            await AddOrUpdatePais("Alemanha", "DE", "DEU", "276", "49", ".de", "de");
            await AddOrUpdatePais("Gana", "GH", "GHA", "288", "233", ".gh", "gh");
            await AddOrUpdatePais("Gibraltar", "GI", "GIB", "292", "350", ".gi", "gi");
            await AddOrUpdatePais("Grécia", "GR", "GRC", "300", "30", ".gr", "gr");
            await AddOrUpdatePais("Gronelândia", "GL", "GRL", "304", "299", ".gl", "gl");
            await AddOrUpdatePais("Grenada", "GD", "GRD", "308", "1+473", ".gd", "gd");
            await AddOrUpdatePais("Guadeloupe", "GP", "GLP", "312", "590", ".gp", "gp");
            await AddOrUpdatePais("Guam", "GU", "GUM", "316", "1+671", ".gu", "gu");
            await AddOrUpdatePais("Guatemala", "GT", "GTM", "320", "502", ".gt", "gt");
            await AddOrUpdatePais("Guernsey", "GG", "GGY", "831", "44", ".gg", null);
            await AddOrUpdatePais("Guiné-Conacri", "GN", "GIN", "324", "224", ".gn", "gn");
            await AddOrUpdatePais("Guiné-Bissau", "GW", "GNB", "624", "245", ".gw", "gw");
            await AddOrUpdatePais("Guiana", "GY", "GUY", "328", "592", ".gy", "gy");
            await AddOrUpdatePais("Haiti", "HT", "HTI", "332", "509", ".ht", "ht");
            await AddOrUpdatePais("Ilha Heard e Ilhas McDonald", "HM", "HMD", "334", null, ".hm", "hm");
            await AddOrUpdatePais("Honduras", "HN", "HND", "340", "504", ".hn", "hn");
            await AddOrUpdatePais("Hong Kong", "HK", "HKG", "344", "852", ".hk", "hk");
            await AddOrUpdatePais("Hungria", "HU", "HUN", "348", "36", ".hu", "hu");
            await AddOrUpdatePais("Islândia", "IS", "ISL", "352", "354", ".is", "is");
            await AddOrUpdatePais("Índia", "IN", "IND", "356", "91", ".in", "in");
            await AddOrUpdatePais("Indonésia", "ID", "IDN", "360", "62", ".id", "id");
            await AddOrUpdatePais("Irão", "IR", "IRN", "364", "98", ".ir", "ir");
            await AddOrUpdatePais("Iraque", "IQ", "IRQ", "368", "964", ".iq", "iq");
            await AddOrUpdatePais("Irlanda", "IE", "IRL", "372", "353", ".ie", "ie");
            await AddOrUpdatePais("Ilha de Man", "IM", "IMN", "833", "44", ".im", null);
            await AddOrUpdatePais("Israel", "IL", "ISR", "376", "972", ".il", "il");
            await AddOrUpdatePais("Itália", "IT", "ITA", "380", "39", ".jm", "jm");
            await AddOrUpdatePais("Jamaica", "JM", "JAM", "388", "1+876", ".jm", "jm");
            await AddOrUpdatePais("Japão", "JP", "JPN", "392", "81", ".jp", "jp");
            await AddOrUpdatePais("Jersey", "JE", "JEY", "832", "44", ".je", null);
            await AddOrUpdatePais("Jordânia", "JO", "JOR", "400", "962", ".jo", "jo");
            await AddOrUpdatePais("Cazaquistão", "KZ", "KAZ", "398", "7", ".kz", "kz");
            await AddOrUpdatePais("Quénia", "KE", "KEN", "404", "254", ".ke", "ke");
            await AddOrUpdatePais("Kiribati", "KI", "KIR", "296", "686", ".ki", "ki");
            await AddOrUpdatePais("Kosovo", "XK", null, null, "381", null, null);
            await AddOrUpdatePais("Kuwait", "KW", "KWT", "414", "965", ".kw", "kw");
            await AddOrUpdatePais("Quirguistão", "KG", "KGZ", "417", "996", ".kg", "kg");
            await AddOrUpdatePais("Laos", "LA", "LAO", "418", "856", ".la", "la");
            await AddOrUpdatePais("Letónia", "LV", "LVA", "428", "371", ".lv", "lv");
            await AddOrUpdatePais("Líbano", "LB", "LBN", "422", "961", ".lb", "lb");
            await AddOrUpdatePais("Lesoto", "LS", "LSO", "426", "266", ".ls", "ls");
            await AddOrUpdatePais("Libéria", "LR", "LBR", "430", "231", ".lr", "lr");
            await AddOrUpdatePais("Líbia", "LY", "LBY", "434", "218", ".ly", "ly");
            await AddOrUpdatePais("Liechtenstein", "LI", "LIE", "438", "423", ".li", "li");
            await AddOrUpdatePais("Lituânia", "LT", "LTU", "440", "370", ".lt", "lt");
            await AddOrUpdatePais("Luxemburgo", "LU", "LUX", "442", "352", ".lu", "lu");
            await AddOrUpdatePais("Macau", "MO", "MAC", "446", "853", ".mo", "mo");
            await AddOrUpdatePais("República da Macedónia", "MK", "MKD", "807", "389", ".mk", "mk");
            await AddOrUpdatePais("Madagáscar", "MG", "MDG", "450", "261", ".mg", "mg");
            await AddOrUpdatePais("Malawi", "MW", "MWI", "454", "265", ".mw", "mw");
            await AddOrUpdatePais("Malásia", "MY", "MYS", "458", "60", ".my", "my");
            await AddOrUpdatePais("Maldivas", "MV", "MDV", "462", "960", ".mv", "mv");
            await AddOrUpdatePais("Mali", "ML", "MLI", "466", "223", ".ml", "ml");
            await AddOrUpdatePais("Malta", "MT", "MLT", "470", "356", ".mt", "mt");
            await AddOrUpdatePais("Ilhas Marshall", "MH", "MHL", "584", "692", ".mh", "mh");
            await AddOrUpdatePais("Martinica", "MQ", "MTQ", "474", "596", ".mq", "mq");
            await AddOrUpdatePais("Mauritânia", "MR", "MRT", "478", "222", ".mr", "mr");
            await AddOrUpdatePais("Maurícia", "MU", "MUS", "480", "230", ".mu", "mu");
            await AddOrUpdatePais("Mayotte", "YT", "MYT", "175", "262", ".yt", "yt");
            await AddOrUpdatePais("México", "MX", "MEX", "484", "52", ".mx", "mx");
            await AddOrUpdatePais("Estados Federados da Micronésia", "FM", "FSM", "583", "691", ".fm", "fm");
            await AddOrUpdatePais("Moldávia", "MD", "MDA", "498", "373", ".md", "md");
            await AddOrUpdatePais("Mónaco", "MC", "MCO", "492", "377", ".mc", "mc");
            await AddOrUpdatePais("Mongólia", "MN", "MNG", "496", "976", ".mn", "mn");
            await AddOrUpdatePais("Montenegro", "ME", "MNE", "499", "382", ".me", "me");
            await AddOrUpdatePais("Montserrat", "MS", "MSR", "500", "1+664", ".ms", "ms");
            await AddOrUpdatePais("Marrocos", "MA", "MAR", "504", "212", ".ma", "ma");
            await AddOrUpdatePais("República Democrática da Coreia(Coreia do Norte)", "KP", "PRK", "408", "850", ".kp", "kp");
            await AddOrUpdatePais("Moçambique", "MZ", "MOZ", "508", "258", ".mz", "mz");
            await AddOrUpdatePais("Myanmar (antiga Birmânia)", "MM", "MMR", "104", "95", ".mm", "mm");
            await AddOrUpdatePais("Namíbia", "NA", "NAM", "516", "264", ".na", "na");
            await AddOrUpdatePais("Nauru", "NR", "NRU", "520", "674", ".nr", "nr");
            await AddOrUpdatePais("Nepal", "NP", "NPL", "524", "977", ".np", "np");
            await AddOrUpdatePais("Países Baixos (Holanda)", "NL", "NLD", "528", "31", ".nl", "nl");
            await AddOrUpdatePais("Nova Caledónia", "NC", "NCL", "540", "687", ".nc", "nc");
            await AddOrUpdatePais("Nova Zelândia (Aotearoa)", "NZ", "NZL", "554", "64", ".nz", "nz");
            await AddOrUpdatePais("Nicarágua", "NI", "NIC", "558", "505", ".ni", "ni");
            await AddOrUpdatePais("Níger", "NE", "NER", "562", "227", ".ne", "ne");
            await AddOrUpdatePais("Nigéria", "NG", "NGA", "566", "234", ".ng", "ng");
            await AddOrUpdatePais("Niue", "NU", "NIU", "570", "683", ".nu", "nu");
            await AddOrUpdatePais("Ilha Norfolk", "NF", "NFK", "574", "672", ".nf", "nf");
            await AddOrUpdatePais("Marianas Setentrionais", "MP", "MNP", "580", "1+670", ".mp", "mp");
            await AddOrUpdatePais("Noruega", "NO", "NOR", "578", "47", ".no", "no");
            await AddOrUpdatePais("Oman", "OM", "OMN", "512", "968", ".om", "om");
            await AddOrUpdatePais("Paquistão", "PK", "PAK", "586", "92", ".pk", "pk");
            await AddOrUpdatePais("Palau", "PW", "PLW", "585", "680", ".pw", "pw");
            await AddOrUpdatePais("Palestina", "PS", "PSE", "275", "970", ".ps", "ps");
            await AddOrUpdatePais("Panamá", "PA", "PAN", "591", "507", ".pa", "pa");
            await AddOrUpdatePais("Papua-Nova Guiné", "PG", "PNG", "598", "675", ".pg", "pg");
            await AddOrUpdatePais("Paraguai", "PY", "PRY", "600", "595", ".py", "py");
            await AddOrUpdatePais("Peru", "PE", "PER", "604", "51", ".pe", "pe");
            await AddOrUpdatePais("Filipinas", "PH", "PHL", "608", "63", ".ph", "ph");
            await AddOrUpdatePais("Pitcairn", "PN", "PCN", "612", null, ".pn", "ph");
            await AddOrUpdatePais("Polónia", "PL", "POL", "616", "48", ".pl", "pl");
            await AddOrUpdatePais("Portugal", "PT", "PRT", "620", "351", ".pt", "pt");
            await AddOrUpdatePais("Porto Rico", "PR", "PRI", "630", "1+939", ".pr", "pr");
            await AddOrUpdatePais("Qatar", "QA", "QAT", "634", "974", ".qa", "qa");
            await AddOrUpdatePais("Reunião", "RE", "REU", "638", "262", ".re", "re");
            await AddOrUpdatePais("Roménia", "RO", "ROU", "642", "40", ".ro", "ro");
            await AddOrUpdatePais("Rússia", "RU", "RUS", "643", "7", ".ru", "ru");
            await AddOrUpdatePais("Ruanda", "RW", "RWA", "646", "250", ".rw", "rw");
            await AddOrUpdatePais("Saint Barthelemy", "BL", "BLM", "652", "590", ".bl", null);
            await AddOrUpdatePais("Santa Helena", "SH", "SHN", "654", "290", ".sh", "sh");
            await AddOrUpdatePais("São Cristóvão e Névis (Saint Kitts e Nevis)", "KN", "KNA", "659", "1+869", ".kn", "kn");
            await AddOrUpdatePais("Santa Lúcia", "LC", "LCA", "662", "1+758", ".lc", "lc");
            await AddOrUpdatePais("Saint Martin", "MF", "MAF", "663", "590", ".mf", null);
            await AddOrUpdatePais("Saint Pierre et Miquelon", "PM", "SPM", "666", "508", ".pm", "pm");
            await AddOrUpdatePais("São Vicente e Granadinas", "VC", "VCT", "670", "1+784", ".vc", "vc");
            await AddOrUpdatePais("Samoa (Samoa Ocidental)", "WS", "WSM", "882", "685", ".ws", "ws");
            await AddOrUpdatePais("San Marino", "SM", "SMR", "674", "378", ".sm", "sm");
            await AddOrUpdatePais("São Tomé e Príncipe", "ST", "STP", "678", "239", ".st", "st");
            await AddOrUpdatePais("Arábia Saudita", "SA", "SAU", "682", "966", ".sa", "sa");
            await AddOrUpdatePais("Senegal", "SN", "SEN", "686", "221", ".sn", "sn");
            await AddOrUpdatePais("Sérvia", "RS", "SRB", "688", "381", ".rs", "rs");
            await AddOrUpdatePais("Seychelles", "SC", "SYC", "690", "248", ".sc", "sc");
            await AddOrUpdatePais("Serra Leoa", "SL", "SLE", "694", "232", ".sl", "sl");
            await AddOrUpdatePais("Singapura", "SG", "SGP", "702", "65", ".sg", "sg");
            await AddOrUpdatePais("Sint Maarten", "SX", "SXM", "534", "1+721", ".sx", null);
            await AddOrUpdatePais("Eslováquia", "SK", "SVK", "703", "421", ".sk", "sk");
            await AddOrUpdatePais("Eslovénia", "SI", "SVN", "705", "386", ".si", "si");
            await AddOrUpdatePais("Ilhas Salomão", "SB", "SLB", "090", "677", ".sb", "sb");
            await AddOrUpdatePais("Somália", "SO", "SOM", "706", "252", ".so", "so");
            await AddOrUpdatePais("África do Sul", "ZA", "ZAF", "710", "27", ".za", "za");
            await AddOrUpdatePais("Ilhas Geórgia do Sul e Sandwich do Sul", "GS", "SGS", "239", "500", ".gs", "gs");
            await AddOrUpdatePais("Coreia do Sul", "KR", "KOR", "410", "82", ".kr", "kr");
            await AddOrUpdatePais("Sudão do Sul", "SS", "SSD", "728", "211", ".ss", null);
            await AddOrUpdatePais("Espanha", "ES", "ESP", "724", "34", ".es", "es");
            await AddOrUpdatePais("Sri Lanka", "LK", "LKA", "144", "94", ".lk", "lk");
            await AddOrUpdatePais("Sudão", "SD", "SDN", "729", "249", ".sd", "sd");
            await AddOrUpdatePais("Suriname", "SR", "SUR", "740", "597", ".sr", "sr");
            await AddOrUpdatePais("Svalbard e Jan Mayen", "SJ", "SJM", "744", "47", ".sj", "sj");
            await AddOrUpdatePais("Suazilândia", "SZ", "SWZ", "748", "268", ".sz", "sz");
            await AddOrUpdatePais("Suécia", "SE", "SWE", "752", "46", ".se", "se");
            await AddOrUpdatePais("Suíça", "CH", "CHE", "756", "41", ".ch", "ch");
            await AddOrUpdatePais("Síria", "SY", "SYR", "760", "963", ".sy", "sy");
            await AddOrUpdatePais("Taiwan", "TW", "TWN", "158", "886", ".tw", "tw");
            await AddOrUpdatePais("Tajiquistão", "TJ", "TJK", "762", "992", ".tj", "tj");
            await AddOrUpdatePais("Tanzânia", "TZ", "TZA", "834", "255", ".tz", "tz");
            await AddOrUpdatePais("Tailândia", "TH", "THA", "764", "66", ".th", "th");
            await AddOrUpdatePais("Timor-Leste", "TL", "TLS", "626", "670", ".tl", "tl");
            await AddOrUpdatePais("Togo", "TG", "TGO", "768", "228", ".tg", "tg");
            await AddOrUpdatePais("Toquelau", "TK", "TKL", "772", "690", ".tk", "tk");
            await AddOrUpdatePais("Tonga", "TO", "TON", "776", "676", ".to", "to");
            await AddOrUpdatePais("Trindade e Tobago", "TT", "TTO", "780", "1+868", ".tt", "tt");
            await AddOrUpdatePais("Tunísia", "TN", "TUN", "788", "216", ".tn", "tn");
            await AddOrUpdatePais("Turquia", "TR", "TUR", "792", "90", ".tr", "tr");
            await AddOrUpdatePais("Turquemenistão", "TM", "TKM", "795", "993", ".tm", "tm");
            await AddOrUpdatePais("Turks e Caicos", "TC", "TCA", "796", "1+649", ".tc", "tc");
            await AddOrUpdatePais("Tuvalu", "TV", "TUV", "798", "688", ".tv", "tv");
            await AddOrUpdatePais("Uganda", "UG", "UGA", "800", "256", ".ug", "ug");
            await AddOrUpdatePais("Ucrânia", "UA", "UKR", "804", "380", ".ua", "ua");
            await AddOrUpdatePais("Emiratos Árabes Unidos", "AE", "ARE", "784", "971", ".ae", "ae");
            await AddOrUpdatePais("Reino Unido da Grã-Bretanha e Irlanda do Norte", "GB", "GBR", "826", "44", ".uk", null);
            await AddOrUpdatePais("Estados Unidos", "US", "USA", "840", "1", ".us", "us");
            await AddOrUpdatePais("Ilhas Menores Distantes dos Estados Unidos", "UM", "UMI", "581", null, null, null);
            await AddOrUpdatePais("Uruguai", "UY", "URY", "858", "598", ".uy", "uy");
            await AddOrUpdatePais("Usbequistão", "UZ", "UZB", "860", "998", ".uz", "uz");
            await AddOrUpdatePais("Vanuatu", "VU", "VUT", "548", "678", ".vu", "vu");
            await AddOrUpdatePais("Vaticano", "VA", "VAT", "336", "39", ".va", "va");
            await AddOrUpdatePais("Venezuela", "VE", "VEN", "862", "58", ".ve", "ve");
            await AddOrUpdatePais("Vietname", "VN", "VNM", "704", "84", ".vn", "vn");
            await AddOrUpdatePais("Ilhas Virgens Britânicas", "VG", "VGB", "092", "1+284", ".vg", "vg");
            await AddOrUpdatePais("Ilhas Virgens Americanas", "VI", "VIR", "850", "1+340", ".vi", "vi");
            await AddOrUpdatePais("Wallis e Futuna", "WF", "WLF", "876", "681", ".wf", "wf");
            await AddOrUpdatePais("Saara Ocidental", "EH", "ESH", "732", "212", ".eh", "eh");
            await AddOrUpdatePais("Iémen", "YE", "YEM", "887", "967", ".ye", "ye");
            await AddOrUpdatePais("Zâmbia", "ZM", "ZMB", "894", "260", ".zm", "zm");
            await AddOrUpdatePais("Zimbabwe", "ZW", "ZWE", "716", "263", ".zw", "zw");
        }

        private async Task SeedEstado()
        {
            var paisId = await _context.Pais
                .Where(x => x.ISO2.Equals("BR"))
                .Select(x => x.PaisId)
                .FirstAsync();

            AddOrUpdateEstado(paisId, "Acre", "AC");
            AddOrUpdateEstado(paisId, "Alagoas", "AL");
            AddOrUpdateEstado(paisId, "Amapá", "AP");
            AddOrUpdateEstado(paisId, "Amazonas", "AM");
            AddOrUpdateEstado(paisId, "Bahia", "BA");
            AddOrUpdateEstado(paisId, "Ceará", "CE");
            AddOrUpdateEstado(paisId, "Distrito Federal", "DF");
            AddOrUpdateEstado(paisId, "Espírito Santo", "ES");
            AddOrUpdateEstado(paisId, "Roraima", "RR");
            AddOrUpdateEstado(paisId, "Goiás", "GO");
            AddOrUpdateEstado(paisId, "Maranhão", "MA");
            AddOrUpdateEstado(paisId, "Mato Grosso", "MT");
            AddOrUpdateEstado(paisId, "Mato Grosso do Sul", "MS");
            AddOrUpdateEstado(paisId, "Minas Gerais", "MG");
            AddOrUpdateEstado(paisId, "Pará", "PA");
            AddOrUpdateEstado(paisId, "Paraíba", "PB");
            AddOrUpdateEstado(paisId, "Paraná", "PR");
            AddOrUpdateEstado(paisId, "Pernambuco", "PE");
            AddOrUpdateEstado(paisId, "Piauí", "PI");
            AddOrUpdateEstado(paisId, "Rio de Janeiro", "RJ");
            AddOrUpdateEstado(paisId, "Rio Grande do Norte", "RN");
            AddOrUpdateEstado(paisId, "Rio Grande do Sul", "RS");
            AddOrUpdateEstado(paisId, "Rondônia", "RO");
            AddOrUpdateEstado(paisId, "Tocantins", "TO");
            AddOrUpdateEstado(paisId, "Santa Catarina", "SC");
            AddOrUpdateEstado(paisId, "São Paulo", "SP");
            AddOrUpdateEstado(paisId, "Sergipe", "SE");
        }

        private async Task AddOrUpdatePais(string nome, string iso2, string iso3, string numCode, string ddi, string ccTLD, string bandeiraFileName)
        {
            var fileName = string.Format("{0}.png", iso2);
            var fullFileName = Path.Combine(_paisDirectoryName, fileName);

            string bandeiraStorageObject = null;

            if (File.Exists(fullFileName))
            {
                var buffer = File.ReadAllBytes(fullFileName);
                var storageObjectFullPath = GetPaisBandeiraStorageFullPath(iso2);
                await _storageHelper.UploadBufferAsync(_appStorageBucketName, storageObjectFullPath, "image/png", buffer);
                bandeiraStorageObject = iso2;
            }

            _context.Pais.AddOrUpdate(x => x.ISO2,
                   new Pais
                   {
                       Nome = nome,
                       ISO2 = string.IsNullOrEmpty(iso2) ? null : iso2.ToUpper(),
                       ISO3 = string.IsNullOrEmpty(iso3) ? null : iso3.ToUpper(),
                       NumCode = numCode,
                       DDI = ddi,
                       ccTLD = string.IsNullOrEmpty(ccTLD) ? null : ccTLD.ToLower(),
                       BandeiraStorageObject = string.IsNullOrEmpty(bandeiraStorageObject) ? null : bandeiraStorageObject.ToUpper(),
                       TipoOrigemDado = TipoOrigemDado.InitialCreate,
                   }
            );
        }

        private void AddOrUpdateEstado(long paisId, string nome, string sigla)
        {
            _context.Estado.AddOrUpdate(x => new { x.PaisId, x.Sigla },
                   new Estado
                   {
                       PaisId = paisId,
                       Nome = nome,
                       Sigla = sigla,
                   }
            );
        }

        private string GetPaisBandeiraStorageFullPath(string storageObject)
        {
            return string.Format("{0}/{1}", BandeiraStorageFolder, storageObject);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        #endregion
    }
}
