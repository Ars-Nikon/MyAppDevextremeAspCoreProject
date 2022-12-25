using System.Security.Cryptography;
using System.Text;

namespace MyAppDevextremeAspCoreProject.Utilities
{
    public class Utils
    {
        public static byte[] GetSHA256(string value)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                var valuebyte = Encoding.Unicode.GetBytes(value);
                return mySHA256.ComputeHash(valuebyte);
            }
        }
    }
}
