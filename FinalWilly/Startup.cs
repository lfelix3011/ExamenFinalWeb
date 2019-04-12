using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalWilly.Startup))]
namespace FinalWilly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
