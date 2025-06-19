using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace StudyHub.Services
{
    public class RSAHelperService :IRSAHelperService

    {
        private readonly RSACryptoServiceProvider PrivateKey;
        private readonly string privateKeyName = "private.key.pem";
        private readonly string privateKeyPath = "RSA";

        public RSAHelperService() => PrivateKey = GetPrivateKeyFromPemFile();

        public string Decrypt(string encKey)
        {
            var decryptedBytes = PrivateKey.Decrypt(Convert.FromBase64String(encKey), false);
            return Encoding.UTF8.GetString(decryptedBytes,0,decryptedBytes.Length);
        }
        private RSACryptoServiceProvider GetPrivateKeyFromPemFile()
        {
            using TextReader privateKeyStringReader = new StringReader(File.ReadAllText(GetPath(privateKeyName, privateKeyPath)));
            AsymmetricCipherKeyPair pemReader = (AsymmetricCipherKeyPair)new PemReader(privateKeyStringReader).ReadObject();
            RSAParameters rsaPrivateCrtKeyParameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)pemReader.Private);
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.ImportParameters(rsaPrivateCrtKeyParameters);
            return rsaCryptoServiceProvider;
        }
        private string GetPath(string fileName, string filePath)
        {
            var path = Path.Combine(".", filePath);
            return Path.Combine(path, fileName);
        }
    }
}
