using System;

namespace Furiza.AspNetCore.WebApp.Configuration
{
    public class ApplicationProfile
    {
        public Guid? ClientId { get; set; }
        public string ErrorPage { get; set; }
        public string DefaultCultureInfo { get; set; }
    }
}