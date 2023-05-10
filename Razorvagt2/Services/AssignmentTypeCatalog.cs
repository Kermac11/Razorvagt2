using Microsoft.Data.SqlClient;
using Razorvagt2.Interfaces;
using Razorvagt2.Models;

namespace Razorvagt2.Services
{
    public class AssignmentTypeCatalog:Connection, IAssignmentTypeCatalog
    {
        private string GetAllSql = "SELECT * from AssignmentType";
        private string GetTypeStringSql = "SELECT * from AssignmentType WHERE AssignmentType_ID = @ID";
        private string GetAllTagFromTypeSql = "SELECT * from AssignmentTypeTag WHERE AssignmentType_ID = @ID";
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

        public AssignmentTypeCatalog(IConfiguration configuration) : base(configuration)
        {

        }

        public Task<bool> CreateAssignmentType(string type)
        {
            throw new NotImplementedException();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(InsertTypeSql,connection))
                {

                }
            }
        }

        public Task<bool> CreateTag(Assignment assignment, string tag)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAssignmentType()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTag(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllTypes()
        {
            throw new NotImplementedException();
        }

        public Task<List<Assignment>> GetAssignmentsOfType(string type)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTypeString(int id)
        {
            throw new NotImplementedException();
        }
    }
}
