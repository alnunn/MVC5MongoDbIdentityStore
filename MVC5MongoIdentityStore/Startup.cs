using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoIdentity.Startup))]
namespace MongoIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
