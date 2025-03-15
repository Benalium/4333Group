using _4333Project.MaximPackage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Documents;

namespace _4333Project {
    public static class DbInteractor {
        public static string connectionString = "Server=localhost\\MSSQLSERVER01;Database=test_DB;Trusted_Connection=True;";
        public static int AddRoles(SqlConnection connection, List<Role> roles) {
            foreach(var role in roles) {
                using(var command = new SqlCommand(
                    "INSERT INTO [role] (name) VALUES (@name)",
                    connection
                )) {
                    command.Parameters.AddWithValue("@name", role.name);
                    command.ExecuteNonQuery();
                }
            }   
            return 0;
        }
        public static int AddUsers(SqlConnection connection, List<User> users) {
            foreach(var user in users) {
                using(var command = new SqlCommand(
                    "INSERT INTO [user] (role_id, full_name, login, password) VALUES (@role, @name, @login, @password)",
                    connection
                )) {
                    command.Parameters.AddWithValue("@role", user.roleId);
                    command.Parameters.AddWithValue("@name", user.fullName);
                    command.Parameters.AddWithValue("@login", user.login);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }
        public static List<T> Data<T>(string tableName, SqlConnection connection, Func<SqlDataReader, T> Create) {
            var instances = new List<T>();
            using(var command = new SqlCommand ("SELECT * FROM " + tableName, connection)) {
                using(var reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        instances.Add(Create(reader));
                    }
                }
            }
            return instances;
        }
        public static object ManageConnection(SqlConnection connection, Func<SqlConnection, object> DoUsingConnection) {
            connection.Open();
            object result = DoUsingConnection(connection);
            connection.Close();
            return result;
        }
    }
}