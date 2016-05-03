using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropertyAnalysisTool.Startup))]

namespace PropertyAnalysisTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}