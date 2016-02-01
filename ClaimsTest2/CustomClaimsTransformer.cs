namespace ClaimsTest2
{
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Security.Claims;

    public class CustomClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            // validate name claim
            var nameClaimValue = incomingPrincipal.Identity.Name;

            if (string.IsNullOrEmpty(nameClaimValue))
            {
                throw new SecurityException("A user with no name???");
            }

            return CreatePrincipal(nameClaimValue);
        }

        private ClaimsPrincipal CreatePrincipal(string userName)
        {
            var likesJavaToo = userName.IndexOf("duncan", StringComparison.InvariantCultureIgnoreCase) > -1;

            var claimsCollection = new List<Claim>
                                               {
                                                   new Claim(ClaimTypes.Name, userName),
                                                   new Claim(
                                                       "http://www.mysite.com/likesjavatoo",
                                                       likesJavaToo.ToString())
                                               };

            return new ClaimsPrincipal(new ClaimsIdentity(claimsCollection, "Custom"));
        }
    }
}
