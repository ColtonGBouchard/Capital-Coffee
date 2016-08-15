using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapitalCoffee.Startup))]
namespace CapitalCoffee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
