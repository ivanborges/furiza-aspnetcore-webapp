using System;
using System.Collections.Generic;
using System.Reflection;

namespace Furiza.AspNetCore.WebApp.Configuration
{
    public class ApplicationProfile
    {
        public Guid? ClientId { get; set; }
        public string ErrorPage { get; set; }
        public string DefaultCultureInfo { get; set; }
        public ICollection<Assembly> AutomapperAssemblies { get; } = new List<Assembly>();
    }
}