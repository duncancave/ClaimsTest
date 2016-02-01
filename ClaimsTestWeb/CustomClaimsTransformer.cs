namespace ClaimsTestWeb
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Services;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;

    public class CustomClaimsTransformer : ClaimsAuthenticationManager
    {
        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="resourceName">An optional variable to describe the resource the user is trying to access.</param>
        /// <param name="incomingPrincipal">The outcome of the authentication; it represents the User that has just been authenticated on the login page</param>
        /// <returns></returns>
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            // If the user is anonymous then we let the base class handle the call.
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }

            var transformedPrincipal = DressUpPrincipal(incomingPrincipal.Identity.Name);

            this.CreateSession(transformedPrincipal);

            return transformedPrincipal;
        }

        private void CreateSession(ClaimsPrincipal transformedPrincipal)
        {
            var sessionSecurityToken = new SessionSecurityToken(transformedPrincipal, TimeSpan.FromHours(8));
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionSecurityToken);
        }

        private ClaimsPrincipal DressUpPrincipal(string userName)
        {
            var claims = new List<Claim>();

            // Simulate database lookup
            if (userName.IndexOf("duncan", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                claims.Add(new Claim(ClaimTypes.Country, "England"));
                claims.Add(new Claim(ClaimTypes.GivenName, "Duncan"));
                claims.Add(new Claim(ClaimTypes.Name, "Duncan"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "Duncan"));
                claims.Add(new Claim(ClaimTypes.Role, "Development"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.GivenName, userName));
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "Duncan"));
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
        }
    }
}