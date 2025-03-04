using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Documents;

namespace _4333Project
{
    public static class DBInteractor
    {
        public static void Copy(
            string[,] list, 
            SqlConnection sqlConnection, 
            string stringCommand = "INSERT INTO user (role, name, login, password) VALUES (@role, @name, @login, @password)"
        ) {
            for (int i = 0; i < list.GetLength(0); i++)
            {
                int j = 0;

                using(SqlCommand command = new SqlCommand(stringCommand, sqlConnection))
                {


                    command.ExecuteNonQuery();
                }
            }
        }

        public static Action<SqlConnection, User, string> AddUser = (connection, user, commandText) => {
            using(var command = new SqlCommand(commandText, connection)) {
                //command.Parameters.AddWithValue("@role", user.Role);
                //command.Parameters.AddWithValue("@name", user.Name);
                //command.Parameters.AddWithValue("@login", user.Login);
                //command.Parameters.AddWithValue("@password", user.Password);
                command.ExecuteNonQuery();
            }
        };

        public static Action<User[], Action<SqlConnection, User, string>> OpenConnection = (users, ExecuteCommand) => {
            var commandText = "INSERT INTO [user] VALUES (2, 3, 4, 5)";
            using(var connection = new SqlConnection(DBInteractor.connectionString)) {
                connection.Open();
                foreach(User user in users) {
                    ExecuteCommand(connection, user, commandText);
                }
            }
        };

        public static string connectionString = "Server=localhost\\MSSQLSERVER01;Database=test_DB;Trusted_Connection=True;";
    }
}