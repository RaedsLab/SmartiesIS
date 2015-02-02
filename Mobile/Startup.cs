using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mobile.Startup))]
namespace Mobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
