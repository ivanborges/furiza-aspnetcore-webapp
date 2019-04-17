using Refit;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Roles
{
    public interface IRolesClient
    {
        [Get("/api/v1/Roles")]
        Task<RolesGetManyResult> GetAsync();

        [Post("/api/v1/Roles")]
        Task PostAsync(RolesPost rolesPost);

        [Delete("/api/v1/Roles/{roleName}")]
        Task DeleteAsync(string roleName);
    }
}