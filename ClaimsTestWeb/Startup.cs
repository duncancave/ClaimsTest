using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClaimsTestWeb.Startup))]
namespace ClaimsTestWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
