using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lshOA.UI.Startup))]
namespace lshOA.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
