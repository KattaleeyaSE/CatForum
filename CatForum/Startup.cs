using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CatForum.Startup))]
namespace CatForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
