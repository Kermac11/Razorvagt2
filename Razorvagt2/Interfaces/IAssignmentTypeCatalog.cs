using Razorvagt2.Models;
namespace Razorvagt2.Interfaces
{
    public interface IAssignmentTypeCatalog
    {
        Task<List<string>> GetAllTypes();

        Task<string> GetTypeString(int id);

        Task<bool> CreateAssignmentType(string type);

        Task<bool> CreateTag(Assignment assignment, string tag);

        Task<bool> DeleteTag(int id);

        Task<string> DeleteAssignmentType();

        Task<List<Assignment>> GetAssignmentsOfType(string type);
    }
}
