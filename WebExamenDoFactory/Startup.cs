using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebExamenDoFactory.Startup))]

namespace WebExamenDoFactory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }


    }
}