using FooBlog.Shared.Configuration.Identity;
using FooBlog.STS.Identity.Configuration.Interfaces;

namespace FooBlog.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {      
        public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
        public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
    }
}





