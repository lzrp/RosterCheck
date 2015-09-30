using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RosterCheck_ASPNET.Startup))]
namespace RosterCheck_ASPNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
