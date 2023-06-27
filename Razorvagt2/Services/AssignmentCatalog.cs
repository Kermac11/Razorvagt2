using Microsoft.Data.SqlClient;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;
using Razorvagt2.Services;

namespace Razorvagt2.Services
{
    public class AssignmentCatalog : Connection, IAssignmentCatalog
    {

        private string GetAllSql = "SELECT * from Assignment";
        private string GetAssignmentFromIDSql = "SELECT * from Assignment WHERE Assignment_ID = @ID";
        private string GetAssignmentFromUserIDSql = "SELECT * from Assignment WHERE User_ID = @UID";
        private string DeleteAssignmentSql = "DELETE from Assignment WHERE Assignment_ID = @ID";
        private string insertAssignmentSql = "INSERT into Assignment Values(@UID, @Dt, @LT, @AT, @SID)";
        private string updateAssignmentSql = "UPDATE Assignment set User_ID = @UID, Date = @DT, Length = @lG, Assignment_Type = @AT, Schedule_ID = @SID WHERE Assignment_ID = @ID";
        /*  [Assignment_ID]   INT          IDENTITY (1, 1) NOT NULL,
            [User_ID]         INT          NULL,
            [Date]            DATETIME     NULL,
            [Length]          BIGINT       NULL,
            [Assignment_Type] VARCHAR (50) NOT NULL,
            [Schedule_ID]*/


        private IUserCatalog _userCatalog;

        public AssignmentCatalog(IConfiguration configuration, IUserCatalog userCatalog) : base(configuration)
        {
            this._userCatalog = userCatalog;
        }

        public AssignmentCatalog(string connectionString, IUserCatalog userCatalog) : base(connectionString)
        {
            this._userCatalog = userCatalog;
        }

        public async Task<bool> CreateAssignment(Assignment assignment)
        {
            //{//@UID, @Dt, @LT, @AT, @SID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertAssignmentSql, connection))
                {
                    try
                    {
                        if (assignment.User == null)
                        {
                            command.Parameters.AddWithValue("@UID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@UID", assignment.User.ID);
                        }
                        command.Parameters.AddWithValue("@Dt", assignment.Date);
                        command.Parameters.AddWithValue("@LT", assignment.Length.Ticks);
                        command.Parameters.AddWithValue("@AT", 1);
                        if (assignment.Schedule == null)
                        {
                            command.Parameters.AddWithValue("@SID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@SID", assignment.Schedule.ID);
                        }
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

        public async Task<Assignment> DeleteAssignment(int assignmentId)
        {
            Assignment assignment = await GetAssignmentFromId(assignmentId);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteAssignmentSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", assignmentId);
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
            return assignment;
        }


        public async Task<List<Assignment>> GetAllAssignments()
        {

            List<Assignment> result = new List<Assignment>();
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
                            Assignment assignment = new Assignment();
                            assignment.ID = reader.GetInt32(i: 0);
                            if (!reader.IsDBNull(i: 1))
                            {
                                int userid = reader.GetInt32(i: 1);

                                assignment.User = await _userCatalog.GetUserFromID(userid);
                            }
                            if (!reader.IsDBNull(i: 2))
                            {
                                assignment.Date = reader.GetDateTime(i: 2);
                            }
                            if (!reader.IsDBNull(i: 3))
                            {
                                long length = reader.GetInt64(i: 3);
                                assignment.Length = TimeSpan.FromTicks(length);
                            }
                            // AssigmentType Katalog needed
                            int assignmentType = reader.GetInt32(i: 4);

                            if (!reader.IsDBNull(i: 5))
                            {
                                //schedulekatalog needed
                                int scheduleId = reader.GetInt32(i: 5);
                            }
                            result.Add(assignment);
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

        public async Task<Assignment> GetAssignmentFromId(int idNumber)
        {
            Assignment assignment = new Assignment();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetAssignmentFromIDSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", idNumber);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            assignment.ID = reader.GetInt32(i: 0);
                            if (!reader.IsDBNull(i: 1))
                            {
                                int userid = reader.GetInt32(i: 1);

                                assignment.User = await _userCatalog.GetUserFromID(userid);
                            }
                            if (!reader.IsDBNull(i: 2))
                            {
                                DateTime date = reader.GetDateTime(i: 2);
                            }
                            if (!reader.IsDBNull(i: 3))
                            {
                                long length = reader.GetInt64(i: 3);
                                assignment.Length = TimeSpan.FromTicks(length);
                            }

                            int assignmentType = reader.GetInt32(i: 4);

                            if (!reader.IsDBNull(i: 5))
                            {
                                int scheduleId = reader.GetInt32(i: 5);
                            }
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
            return assignment;
        }

        public async Task<List<Assignment>> GetAssignmentFromUser(User user)
        {
            Assignment assignment = new Assignment();
            List<Assignment> result = new List<Assignment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetAssignmentFromUserIDSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@UID", user.ID);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            assignment.ID = reader.GetInt32(i: 0);
                            if (!reader.IsDBNull(i: 1))
                            {
                                int userid = reader.GetInt32(i: 1);

                                assignment.User = await _userCatalog.GetUserFromID(userid);
                            }
                            if (!reader.IsDBNull(i: 2))
                            {
                                DateTime date = reader.GetDateTime(i: 2);
                            }
                            if (!reader.IsDBNull(i: 3))
                            {
                                long length = reader.GetInt64(i: 3);
                                assignment.Length = TimeSpan.FromTicks(length);
                            }

                            int assignmentType = reader.GetInt32(i: 4);

                            if (!reader.IsDBNull(i: 5))
                            {
                                int scheduleId = reader.GetInt32(i: 5);
                            }
                            result.Add(assignment);
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


        public async Task<bool> UpdateAssignement(int assignmentId, Assignment assignment)
        {
            // "UPDATE Assignment set User_ID = @UID, Date = @DT, Length = @lG, Assignment_Type = @AT, Schedule_ID = @SID WHERE Assignment_ID = @ID";

            //@UID, @DT,@lG,  @AT, @SID WHERE Assignment_ID = @ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateAssignmentSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@ID", assignmentId);
                        command.Parameters.AddWithValue("@UID", assignment.User.ID);
                        command.Parameters.AddWithValue("@DT", assignment.Date);
                        command.Parameters.AddWithValue("@LG", assignment.Length);
                        command.Parameters.AddWithValue("@AT", assignment.AssignmentType);
                        command.Parameters.AddWithValue("@SID", assignment.Schedule.ID);
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
