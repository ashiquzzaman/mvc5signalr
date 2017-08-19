using Microsoft.Owin;
using Owin;
using SignalRMVCUnityCRUD;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalRMVCUnityCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

        }
    }
}
