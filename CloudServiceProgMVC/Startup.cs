using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudServiceProgMVC.Startup))]
namespace CloudServiceProgMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
