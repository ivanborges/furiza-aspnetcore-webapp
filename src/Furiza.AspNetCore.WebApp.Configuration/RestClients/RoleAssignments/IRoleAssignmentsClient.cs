using Refit;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.RoleAssignments
{
    public interface IRoleAssignmentsClient
    {
        [Post("/api/v1/RoleAssignments")]
        Task PostAsync(RoleAssignmentsPost roleAssignmentsPost);

        [Delete("/api/v1/RoleAssignments")]
        Task DeleteAsync([Body]RoleAssignmentsDelete roleAssignmentsDelete);
    }
}