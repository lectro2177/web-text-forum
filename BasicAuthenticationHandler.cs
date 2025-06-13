using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using web_text_forum.Application.Interfaces;
using web_text_forum.Models;

namespace web_text_forum.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userRepository)
            : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            User? user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                    return AuthenticateResult.Fail("Invalid Authorization Scheme");

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                if (credentials.Length != 2)
                    return AuthenticateResult.Fail("Invalid Authorization Header");

                var username = credentials[0];
                var password = credentials[1];

                user = await _userRepository.GetUserByUsernameAsync(username);
                if (user == null)
                    return AuthenticateResult.Fail("Invalid Username or Password");

                // Assume user.PasswordHash is a hashed password and you have a method to verify it
                if (!PasswordHasher.Verify(password, user.PasswordHash))
                    return AuthenticateResult.Fail("Invalid Username or Password");
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace web_text_forum.Security
{
    public static class PasswordHasher
    {
        public static bool Verify(string password, string hashedPassword)
        {
            // For demo: SHA256 hash comparison. Replace with a proper password hasher in production.
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashString = Convert.ToBase64String(hash);
            return hashString == hashedPassword;
        }
    }
}
using Microsoft.AspNetCore.Authorization;

namespace web_text_forum.Attributes
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public BasicAuthorizeAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
// In ConfigureServices method:
services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, Security.BasicAuthenticationHandler>("BasicAuthentication", null);

services.AddAuthorization(options =>
{
    options.AddPolicy("BasicAuthentication", policy =>
        policy.RequireAuthenticatedUser());
});
using web_text_forum.Attributes;

[BasicAuthorize]
[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    // ... rest of the class unchanged ...
}
