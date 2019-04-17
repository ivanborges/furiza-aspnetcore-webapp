using System.ComponentModel.DataAnnotations;

namespace Furiza.AspNetCore.WebApp.Configuration
{
    public class reCaptchaConfiguration
    {
        [Required]
        public string SiteKey { get; set; }

        [Required]
        public string SecretKey { get; set; }
    }
}