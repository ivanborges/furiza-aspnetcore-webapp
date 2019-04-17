using Refit;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Users
{
    public interface IUsersClient
    {
        [Get("/api/v1/Users")]
        Task<UsersGetManyResult> GetAsync(UsersGetMany usersGetMany);

        [Get("/api/v1/Users/{userName}")]
        Task<UsersGetResult> GetAsync(string userName);

        [Get("/api/v1/Users/byEmail")]
        Task<UsersGetResult> GetByEmailAsync(UsersGetByEmail usersGetByEmail);

        [Post("/api/v1/Users")]
        Task PostAsync(UsersPost usersPost);

        [Post("/api/v1/Users/ChangePassword")]
        Task ChangePasswordPostAsync(ChangePasswordPost changePasswordPost);

        [Post("/api/v1/Users/{userName}/ResetPassword")]
        Task<UsersResetPasswordPostResult> ResetPasswordPostAsync(string userName);

        [Post("/api/v1/Users/{userName}/ConfirmEmail")]
        Task ConfirmEmailPostAsync(string userName);

        [Post("/api/v1/Users/{userName}/Lock")]
        Task LockPostAsync(string userName);

        [Post("/api/v1/Users/{userName}/Unlock")]
        Task UnlockPostAsync(string userName);

        [Delete("/api/v1/Users/{userName}")]
        Task DeleteAsync(string userName);

        [Post("/api/v1/Users/{userName}/Claims")]
        Task ModifyClaimPostAsync(string userName, ModifyClaimPost modifyClaimPost);
    }
}