using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage {
    public static class Procedures {
        public static void AddUsers(List<BadUser> badUsers) {
            using(var sqlConnection = new SqlConnection(DbInteractor.connectionString)) {
                DbInteractor.ManageConnection(sqlConnection, connection => 
                    DbInteractor.AddRoles(
                        connection, 
                        Convertor.ConvertToRoles(badUsers)
                    )
                );

                DbInteractor.ManageConnection(sqlConnection, connection => 
                    DbInteractor.AddUsers(sqlConnection, 
                        Convertor.ConvertToUsers(
                            badUsers,
                            DbInteractor.Data(
                                "[role]", 
                                connection, 
                                reader => new Role(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name"))
                                )
                            )
                        )
                    )
                );
            }
        }
        public static string DialogFileName() {
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                DefaultExt = "*.xls;*.xlsx",
                Filter = "файл Excel (Spisok.xlsx)|*.xlsx",
                Title = "Выберите файл базы данных"
            };
            openFileDialog.ShowDialog();  // implicitly changes `FileName` property of the openFileDialog object
            return openFileDialog.FileName;
        }
        public static (List<Role>, List<User>) RolesAndUsersFromDb() {
            using(var sqlConnection = new SqlConnection(DbInteractor.connectionString)) {
                return 
                    ( 
                        (List<Role>)DbInteractor.ManageConnection(
                            sqlConnection,
                            connection => DbInteractor.Data(
                                "[role]", 
                                connection,
                                reader => new Role(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name"))
                                )
                            ) 
                        ),
                        (List<User>)DbInteractor.ManageConnection(
                            sqlConnection,
                            connection => DbInteractor.Data(
                                "[user]", 
                                connection,
                                reader => new User(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("full_name")),
                                    reader.GetString(reader.GetOrdinal("login")),
                                    reader.GetString(reader.GetOrdinal("password")),
                                    reader.GetInt32(reader.GetOrdinal("role_id"))
                                )
                            )
                        )
                    );
            }
        }
    }
}
