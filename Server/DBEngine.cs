using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Server
{
    public class TokenGenerator
    {
        public static string GenerateToken(int size)
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
    public class DBEngine
    {
        public static string dbPath= "./DB.json";
        public static Dictionary<string, string> DB = new Dictionary<string, string>();
        public void DBInit()
        {

            DB.Add("hi", "hey");
            DB.Add("yowhatup", "yoooo");
            if (File.Exists(dbPath))
            {
                var data = File.ReadAllText(dbPath);
                DB = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            }
        }
        private void PreserveDB()
        {
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath);
            }
//            File.WriteAllText(dbPath, JsonConvert.SerializeObject(DB));

        }
        public bool DBLookup(string lookup)
        {
            return DB.ContainsKey(lookup);
        }
        public void AddData(string tokenToAdd)
        {
            DB.Add(tokenToAdd, null);
            PreserveDB();
        }
    } 
}