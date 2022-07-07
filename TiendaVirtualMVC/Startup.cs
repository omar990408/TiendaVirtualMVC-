using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaVirtualMVC.Startup))]
namespace TiendaVirtualMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
