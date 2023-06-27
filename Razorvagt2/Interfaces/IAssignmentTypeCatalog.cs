using Razorvagt2.Models;
namespace Razorvagt2.Interfaces
{
    public interface IAssignmentTypeCatalog
    {
        Task<List<Tag>> GetAllTypes();

        Task<string> GetTypeString(int id);

        Task<Tag> GetTag(int id);

        Task<bool> CreateAssignmentType(int id);

        Task<bool> CreateTag(Assignment assignment, string tag);

        Task<Tag> DeleteTag(int id);

        Task<string> DeleteAssignmentType(int id);

        Task<List<Assignment>> GetAssignmentsOfType(string type);
    }
}
