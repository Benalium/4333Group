using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage {
    public class BadUser {
        [JsonPropertyName("Position")]
        public string role;
        [JsonPropertyName("FullName")]
        public string fullName;
        [JsonPropertyName("Log")]
        public string login;
        [JsonPropertyName("Password")]
        public string password;
        public BadUser (string fullName, string login, string password, string role) { 
            this.fullName = fullName; 
            this.login = login;
            this.password = password;
            this.role = role;
        }
    }
}
