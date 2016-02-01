namespace ClaimsTest
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;

    class Program
    {
        private static void Main(string[] args)
        {
            Setup();
            CheckCompatibility();
            CheckNewClaimsUsage();

            Console.ReadLine();
        }

        private static void Setup()
        {
            IList<Claim> claimCollection = new List<Claim>
                                               {
                                                   new Claim(ClaimTypes.Name, "Duncan"),
                                                   new Claim(ClaimTypes.Country, "England"),
                                                   new Claim(ClaimTypes.Gender, "M"),
                                                   new Claim(ClaimTypes.Surname, "Cave"),
                                                   new Claim(ClaimTypes.Email, "hello@me.com"),
                                                   new Claim(ClaimTypes.Role, "Development")
                                               };

            // Without a string here you will be anonymous and not authenticated
            var claimsIdentity = new ClaimsIdentity(claimCollection, "String to make you authenticated", ClaimTypes.Email, ClaimTypes.Role);

            Console.WriteLine(claimsIdentity.IsAuthenticated);

            var principal = new ClaimsPrincipal(claimsIdentity);
            Thread.CurrentPrincipal = principal;
        }

        // This is the old way to do things
        private static void CheckCompatibility()
        {
            var currentPrincipal = Thread.CurrentPrincipal;
            Console.WriteLine(currentPrincipal.Identity.Name);
            Console.WriteLine(currentPrincipal.IsInRole("Development"));
        }

        // This is the new way to do things
        private static void CheckNewClaimsUsage()
        {
            var currentClaimsPrincipal = ClaimsPrincipal.Current;
            var nameClaim = currentClaimsPrincipal.FindFirst(ClaimTypes.Name);
            Console.WriteLine(nameClaim.Value);

            // Principle can have multiple identities
            foreach (var claimsIdentity in currentClaimsPrincipal.Identities)
            {
                Console.WriteLine(claimsIdentity.Name);
            }
        }
    }
}
