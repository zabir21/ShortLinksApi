using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShortLinksApi.Configuration;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BasicAuthentication.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IOptions<AuthorizationBasic> _authorization;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<AuthorizationBasic> authorization) : base(options, logger, encoder, clock)
        {
            _authorization = authorization;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                // Проверяем учетные данные против вашей системы учетных данных (например, базы данных или хранилища пользователей)
                if (IsUserValid(username, password))
                {
                    var claims = new Claim[]
                    {
                     new Claim(ClaimTypes.Name, username),
                    };

                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            bool IsUserValid(string username, string password)
            {

                // Проверяем, есть ли пользователь в хранилище
                if (_authorization.Value.UserName == username && _authorization.Value.Password == password)
                {
                    return true;
                }

                return false; // Учетные данные не соответствуют
            }
        }
    }
}
