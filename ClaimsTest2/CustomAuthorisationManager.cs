namespace ClaimsTest2
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
                var likesJava = context.Principal.HasClaim("http://www.mysite.com/likesjavatoo", "True");
                return likesJava;
            }

            return false;
        }
    }
}
