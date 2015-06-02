using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompraInteligente.Startup))]
namespace CompraInteligente
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
