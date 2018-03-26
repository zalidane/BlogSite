using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zalidane.com.Startup))]
namespace Zalidane.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
