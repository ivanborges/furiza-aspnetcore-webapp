using System.ComponentModel.DataAnnotations;

namespace Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer
{
    public class AuthenticationConfigurationJwt
    {
        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public string Secret { get; set; }
    }
}