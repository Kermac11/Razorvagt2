using Microsoft.Data.SqlClient;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;
using Razorvagt2.Services;

namespace Razorvagt2.Services
{
    public class AssignmentTypeCatalog : Connection, IAssignmentTypeCatalog
    {
        private string GetAllSql = "SELECT * from AssignmentType";
        private string GetTypeStringSql = "SELECT * from AssignmentType WHERE AssignmentType_ID = @ID";
        private string GetAllTagFromTypeSql = "SELECT * from AssignmentTypeTag WHERE AssignmentTypeTag_ID = @ID";
        private string GetAllFromTypeSql = "SELECT * from AssignmentTypeTag WHERE AssignmentType_ID = @ID";
        private string GetAllTagFromAssignmentSql = "SELECT * from AssignmentTypeTag WHERE Assignment_ID = @ID";
        private string DeleteTypeSql = "DELETE from AssignmentType WHERE AssignmentType_ID = @ID";
        private string DeleteTagSql = "DELETE from AssignmentTypeTag WHERE AssignmentTypeTag_ID = @ID";
        private string InsertTypeSql = "INSERT into AssignmentType values(@Type)";
        private string InsertTagSql = "INSERT intro AssignmentTypeTag values (@A,@Type)";


        /*CREATE TABLE [dbo].[AssignmentType] (
    [AssignmentType_ID] INT          IDENTITY (1, 1) NOT NULL,
    [Type]              VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AssignmentType_ID] ASC)*/

        /*CREATE TABLE [dbo].[AssignmentTypeTag] (
    [AssignmentTypeTag_ID] INT IDENTITY (1, 1) NOT NULL,
    [AssignmentType_ID]    INT NOT NULL,
    [Assignment_ID]        INT NOT NULL,
    CONSTRAINT [PK_AssignmentTypeTag] PRIMARY KEY CLUSTERED ([AssignmentTypeTag_ID] ASC),
    CONSTRAINT [AssignmentType_ID] FOREIGN KEY ([AssignmentType_ID]) REFERENCES [dbo].[AssignmentType] ([AssignmentType_ID]),
    CONSTRAINT [Assignment_ID] FOREIGN KEY ([Assignment_ID]) REFERENCES [dbo].[Assignment] ([Assignment_ID])*/
        private IAssignmentCatalog _acat;

        public AssignmentTypeCatalog(IConfiguration configuration, IAssignmentCatalog assignmentCatalog) : base(configuration)
        {
            this._acat = assignmentCatalog;
        }

        public async Task<bool> CreateAssignmentType(string type)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(InsertTypeSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Type", type);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
        }

        public Task<bool> CreateAssignmentType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateTag(Assignment assignment, string tag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(InsertTypeSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@A", assignment.ID);
                        command.Parameters.AddWithValue("@Type", tag);
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
        }

        public async Task<string> DeleteAssignmentType(int id)
        {
            //  AssignmentType_ID = @ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteTypeSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        return GetTypeString(id).Result;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
        }

        public async Task<Tag> DeleteTag(int id)
        {
            //  AssignmentType_ID = @ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteTagSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        return GetTag(id).Result;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
        }

        public async Task<List<Tag>> GetAllTypes()
        {
            List<Tag> result = new List<Tag>();
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
                            int typeid = reader.GetInt32(i: 0);
                            string name = reader.GetString(i: 1);
                            Tag tag = new Tag(typeid, name);
                            result.Add(tag);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
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

        public Task<List<Assignment>> GetAssignmentsOfType(int id)
        {
            throw new NotImplementedException();

        }

        public async Task<List<Assignment>> GetAssignmentsOfType(string type)
        {
            List<Assignment> alist = _acat.GetAllAssignments().Result.FindAll(l => l.AssignmentType == type);
            return alist;
        }

        //private string GetTypeStringSql = "SELECT * from AssignmentType WHERE AssignmentType_ID = @ID";
        //private string GetAllTagFromTypeSql = "SELECT * from AssignmentTypeTag WHERE AssignmentType_ID = @ID";

        public async Task<Tag> GetTag(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetAllTagFromTypeSql))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int tagid = reader.GetInt32(i: 0);
                            int typeid = reader.GetInt32(i:1);
                            int asignmentid = reader.GetInt32(i: 2);
                            Tag tag = new Tag(typeid, GetTypeString(typeid).Result) ;
                            return tag;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
        }

        public async Task<string> GetTypeString(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetTypeStringSql))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int typeid = reader.GetInt32(i: 0);
                            string type = reader.GetString(i: 1);
                            return type;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
        }
    }
}
