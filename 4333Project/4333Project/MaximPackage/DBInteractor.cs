using _4333Project.MaximPackage;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Documents;

namespace _4333Project {
    public static class DBInteractor {
        public static Action<SqlConnection, string[,]> AddUser = (connection, data) => {
            var users = Convertor.Users(data);
            var commandText = "INSERT INTO [user] VALUES (@role, @name, @login, @password)";
            using(var command = new SqlCommand(commandText, connection)) {
                foreach(User user in users) {
                    command.Parameters.AddWithValue("@role", user.roleId);
                    command.Parameters.AddWithValue("@name", user.fullName);
                    command.Parameters.AddWithValue("@login", user.login);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.ExecuteNonQuery();
                }
            }
        };
    }
}