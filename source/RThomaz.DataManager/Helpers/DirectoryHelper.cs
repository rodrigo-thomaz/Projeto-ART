using System.Globalization;
using System.IO;
using System.Text;

namespace RThomaz.DataManager.Helpers
{
    public static class DirectoryHelper
    {
        public static Encoding Encoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage);
        public static string DefaultSampleDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string NomesSampleDirectory = Path.Combine(DefaultSampleDirectory, @"Samples\Nomes\");
        public static string SobrenomesSampleDirectory = Path.Combine(DefaultSampleDirectory, @"Samples\Sobrenomes\");
    }
}
