using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OfficeDocumentGeneration.Startup))]
namespace OfficeDocumentGeneration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
