using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace _4333Project.MaximPackage {
    public static class Convertor {
        public static List<User> ConvertToUsers (List<BadUser> badUsers, List<Role> roles) {
            var users = new List<User>();
            badUsers
                .ForEach(u => users.Add(new User(
                u.fullName, 
                u.login, 
                Hasher.Hash(u.password), 
                roles.First(r => u.role == r.name).id
            )));
            return users;
        }
        public static List<Role> ConvertToRoles (List<BadUser> badUsers) {
            var roles = new List<Role>();
            badUsers
                .Select(u => u.role)
                .Distinct();
            return roles;
        }
    }
}