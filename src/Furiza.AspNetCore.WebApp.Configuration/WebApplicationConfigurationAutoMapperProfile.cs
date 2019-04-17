using AutoMapper;
using Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer.Services;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.Auth;

namespace Furiza.AspNetCore.WebApp.Configuration
{
    public class WebApplicationConfigurationAutoMapperProfile : Profile
    {
        public WebApplicationConfigurationAutoMapperProfile()
        {
            CreateMap<AuthPostResult, RefreshTokenResult>();
        }
    }
}