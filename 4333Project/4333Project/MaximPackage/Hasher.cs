using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage {
    public static class Hasher {
        public static string Hash(string @string) {
            var bytes = Encoding.UTF8.GetBytes(@string);
            byte[] hash;
            using(var sha256 = SHA256.Create()) {
                hash = sha256.ComputeHash(bytes); 
            }
            var builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

