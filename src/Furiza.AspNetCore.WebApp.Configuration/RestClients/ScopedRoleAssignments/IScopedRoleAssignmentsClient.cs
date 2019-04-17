using Refit;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.ScopedRoleAssignments
{
    public interface IScopedRoleAssignmentsClient
    {
        [Post("/api/v1/ScopedRoleAssignments")]
        Task PostAsync(ScopedRoleAssignmentsPost scopedRoleAssignmentsPost);

        [Delete("/api/v1/ScopedRoleAssignments")]
        Task DeleteAsync(ScopedRoleAssignmentsDelete scopedRoleAssignmentsDelete);
    }
}