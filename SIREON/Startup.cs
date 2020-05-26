using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIREON.Startup))]
namespace SIREON
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
