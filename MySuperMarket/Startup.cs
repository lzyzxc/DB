using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySuperMarket.Startup))]
namespace MySuperMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
