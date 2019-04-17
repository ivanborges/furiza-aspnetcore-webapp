using AutoMapper;
using Furiza.AspNetCore.WebApp.Authentication.CookiesByJwtBearer.Services;
using Furiza.AspNetCore.WebApp.Configuration.RestClients.Auth;
using System;
using System.Threading.Tasks;

namespace Furiza.AspNetCore.WebApp.Configuration.Services
{
    internal class AccessTokenRefresher : IAccessTokenRefresher
    {
        private readonly ISecurityProviderClient securityProviderClient;
        private readonly IMapper mapper;

        public AccessTokenRefresher(ISecurityProviderClient securityProviderClient,
            IMapper mapper)
        {
            this.securityProviderClient = securityProviderClient ?? throw new ArgumentNullException(nameof(securityProviderClient));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RefreshTokenResult> RefreshAsync(Guid clientId, string refreshToken)
        {
            var authPost = new AuthPost()
            {
                GrantType = GrantType.RefreshToken,
                ClientId = clientId,
                RefreshToken = refreshToken
            };

            var authPostResult = await securityProviderClient.AuthAsync(authPost);
            var refreshTokenResult = mapper.Map<AuthPostResult, RefreshTokenResult>(authPostResult);

            return refreshTokenResult;
        }
    }
}