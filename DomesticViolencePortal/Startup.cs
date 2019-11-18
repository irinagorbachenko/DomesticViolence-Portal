using Microsoft.Owin;
using Owin;



[assembly: OwinStartupAttribute(typeof(DomesticViolencePortal.Startup))]
namespace DomesticViolencePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}