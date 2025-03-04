using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage {
    public static class Convertor {
        public static List<User> Users(string[,] data) {
            var users = new List<User>();
            for(int i = 0; i < data.GetLength(0); i++) users.Add(new User { 
                roleId = Convert.ToInt32(data[i, 0]), 
                fullName = data[i, 1], 
                login = data[i, 2], 
                password = data[i, 3]
            });
            return users; 
        }
    }
}
