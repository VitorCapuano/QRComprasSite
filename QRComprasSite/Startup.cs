using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QRComprasSite.Startup))]
namespace QRComprasSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
