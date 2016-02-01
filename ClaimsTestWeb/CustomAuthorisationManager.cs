namespace ClaimsTestWeb
{
    using System.Linq;
    using System.Security.Claims;

    public class CustomAuthorisationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;

            if (action == "Show" && resource == "Code")
            {
                var livesInEngland = context.Principal.HasClaim(ClaimTypes.Country, "England");
                var isDuncan = context.Principal.HasClaim(ClaimTypes.GivenName, "Duncan");
                return isDuncan && livesInEngland;
            }

            return false;
        }
    }
}