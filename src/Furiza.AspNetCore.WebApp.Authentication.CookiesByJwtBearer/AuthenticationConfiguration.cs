﻿using System.ComponentModel.DataAnnotations;

namespace Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer
{
    public class AuthenticationConfiguration
    {
        [Required]
        public AuthenticationConfigurationJwt Jwt { get; set; }

        [Required]
        public string SecurityProviderApiUrl { get; set; }        

        public string CookieName { get; set; }
        public string LoginPath { get; set; }
        public string AccessDeniedPath { get; set; }
    }
}