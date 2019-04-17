using Refit;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.ReCaptcha
{
    public interface IReCaptchaClient
    {
        [Get("/recaptcha/api/siteverify")]
        Task<SiteVerifyGetResult> SiteVerifyAsync(string secret, string response);
    }
}