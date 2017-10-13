namespace ART.Infra.CrossCutting.Utils
{
    using System;
    using System.Linq;

    public static class RandonHelper
    {
        #region Fields

        private static Random random = new Random();

        #endregion Fields

        #region Methods

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion Methods
    }
}