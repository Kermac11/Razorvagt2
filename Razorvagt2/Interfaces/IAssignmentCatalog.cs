using Razorvagt2.Models;
namespace Razorvagt2.Interfaces
{
    public interface IAssignmentCatalog
    {
        Task<List<Assignment>> GetAllAssignments();

        Task<Assignment> GetAssignmentFromId(int idNumber);

        Task<List<Assignment>> GetAssignmentFromUser(User user);

        Task<bool> CreateAssignment(Assignment assignment);

        Task<bool> UpdateAssignement(int assignmentId, Assignment assignment );

        Task<Assignment> DeleteAssignment(int assignmentId);
    }
}
