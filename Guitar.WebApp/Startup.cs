using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Guitar.WebApp.Startup))]
namespace Guitar.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
