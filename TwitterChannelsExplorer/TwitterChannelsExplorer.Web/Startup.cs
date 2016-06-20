using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterChannelsExplorer.Web.Web.Startup))]
namespace TwitterChannelsExplorer.Web.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
         
        }
    }
}
