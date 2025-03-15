using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using _4333Project.MaximPackage;

namespace _4333Project {
    public class User {
        public int id;
        public string fullName;
        public string login;
        public string password;
        public int roleId;
        public User(string fullName, string login, string password,int roleId) {
            this.fullName = fullName;
            this.login = login;
            this.password = password;
            this.roleId = roleId;
        }
        public User(int id, string fullName, string login, string password,int roleId) {
            this.id = id;
            this.fullName = fullName;
            this.login = login;
            this.password = password;
            this.roleId = roleId;
        }
    }
}
