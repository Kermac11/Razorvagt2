using Microsoft.Data.SqlClient;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Services
{
    public class UserCatalog : Connection, IUserCatalog
    {
        private string GetAllSql = "SELECT * from Users";
        private string GetUserFromIDSql = "SELECT * from Users WHERE User_ID = @ID";
        private string DeleteUserSql = "DELETE from Users WHERE User_ID = @ID";
        private string insertUserSql = "INSERT into Users Values(@Name, @UN, @PW, @PH, @ML, @AD)";
        private string updateUserSql = "UPDATE Users set Name = @Name, Username = @UN, Password = @PW, Phone = @PH, Email = @ML, Admin = @AD WHERE User_ID = @ID";

        public UserCatalog(IConfiguration configuration) : base(configuration)
        {
        }

        /*  [User_ID]  INT          IDENTITY (1, 1) NOT NULL,
            [Name]     VARCHAR (50) NOT NULL,
            [Username] VARCHAR (50) NOT NULL,
            [Password] VARCHAR (50) NOT NULL,
            [Phone]    VARCHAR (50) NULL,
            [Email]    VARCHAR (50) NOT NULL,
            [Admin]    BIT          NOT NULL,
            PRIMARY KEY CLUSTERED ([User_ID] ASC) */
        public async Task<bool> CreateUser(User user)
        {
            //@Name, @UN, @PW, @PH, @ML, @AD

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertUserSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@UN", user.Username);
                        command.Parameters.AddWithValue("@PW", user.Password);
                        command.Parameters.AddWithValue("@PH", user.Phone);
                        command.Parameters.AddWithValue("@ML", user.EMail);
                        command.Parameters.AddWithValue("@AD", user.Admin);
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return false;
        }

        public async Task<User> DeleteUser(int userid)
        {
            User user = await GetUserFromID(userid);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteUserSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", userid);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return user;
        }

        public Task<User> DeleteUser(int userid, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> result = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetAllSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            User user = new User();
                            user.ID = reader.GetInt32(i: 0);
                            user.Name = reader.GetString(i: 1);
                            user.Username = reader.GetString(i: 2);
                            user.Password = reader.GetString(i: 3);
                            if (!reader.IsDBNull(i: 4))
                            {
                                user.Phone = reader.GetString(i: 4);
                            }
                            user.EMail = reader.GetString(i: 5);
                            user.Admin = reader.GetBoolean(i: 6);
                            result.Add(user);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
            return result;
        }

        public async Task<User> GetUserFromID(int ID)
        {
            /*  [User_ID]  INT          IDENTITY (1, 1) NOT NULL,
            [Name]     VARCHAR (50) NOT NULL,
            [Username] VARCHAR (50) NOT NULL,
            [Password] VARCHAR (50) NOT NULL,
            [Phone]    VARCHAR (50) NULL,
            [Email]    VARCHAR (50) NOT NULL,
            [Admin]    BIT          NOT NULL,
            PRIMARY KEY CLUSTERED ([User_ID] ASC) */
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetUserFromIDSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            user.ID = reader.GetInt32(i: 0);
                            user.Name = reader.GetString(i: 1);
                            user.Username = reader.GetString(i: 2);
                            user.Password = reader.GetString(i: 3);
                            if (!reader.IsDBNull(i: 4))
                            {
                                user.Phone = reader.GetString(i: 4);
                            }
                            user.EMail = reader.GetString(i: 5);
                            user.Admin = reader.GetBoolean(i: 6);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            }
            return user;
        }

        public async Task<bool> UpdateUser(int userid, User user)
        {
            // @ID @Name, @UN, @PW, @PH, @ML, @AD
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateUserSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@ID", userid);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@UN", user.Username);
                        command.Parameters.AddWithValue("@PW", user.Password);
                        command.Parameters.AddWithValue("@PH", user.Phone);
                        command.Parameters.AddWithValue("@ML", user.EMail);
                        command.Parameters.AddWithValue("@AD", user.Admin);
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return false;
                    }

                }
            }
            return false;
        }
    }

}
