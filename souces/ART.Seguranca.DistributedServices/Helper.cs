namespace ART.Seguranca.DistributedServices
{
    using System;
    using System.Security.Cryptography;

    public class Helper
    {
        #region Methods

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        #endregion Methods
    }
}