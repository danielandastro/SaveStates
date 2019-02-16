using System;
using System.Security.Cryptography;
using System.Text;
using LiteDB;

namespace Server
{
    public class TokenGenerator
    {
        public static string Generate(int size)
        {
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
    public static class TokenManager
    {
        public static void AddToken(string tokenToAdd) {


        }
        public static string token;
        public static bool TokenVerifier(string tokenToVerify)
        {
            var db = new LiteDatabase(@"TokenStore.db");
            var col = db.GetCollection<TokenStore>("tokens");

            var results = col.Find(x => x.token.Equals(tokenToVerify));
            if (!results.Equals(null)) { return false; }
            else return true;
        }
    }
    public class TokenStore
    {
        public string token;
        public int id;
    }
}
