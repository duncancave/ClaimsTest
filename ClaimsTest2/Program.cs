namespace ClaimsTest2
{
    using System;
    using System.IdentityModel.Services;
    using System.Security.Permissions;
    using System.Security.Principal;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            SetCurrentPrincipal();
            UseCurrentPrincipal();

            Console.ReadLine();
        }

        private static void UseCurrentPrincipal()
        {
            ShowMeTheCode();
        }

        private static void SetCurrentPrincipal()
        {
            var incomingPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            // This will call our CustomClaimsTransformer (hooked up in App.Config) to add the claims required for this application.
            Thread.CurrentPrincipal =
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager
                    .Authenticate("none", incomingPrincipal);
        }

        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Show", Resource = "Code")]
        private static void ShowMeTheCode()
        {
            Console.WriteLine("Console.WriteLine");
        }
    }
}
