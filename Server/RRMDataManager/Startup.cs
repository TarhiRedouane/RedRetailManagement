using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RRMDataManager.Startup))]

namespace RRMDataManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
