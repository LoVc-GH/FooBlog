using FooBlog.Admin.Configuration.Interfaces;

namespace FooBlog.Admin.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public AdminConfiguration AdminConfiguration { get; set; } = new AdminConfiguration();
        public IdentityDataConfiguration IdentityDataConfiguration { get; set; } = new IdentityDataConfiguration();
        public IdentityServerDataConfiguration IdentityServerDataConfiguration { get; set; } = new IdentityServerDataConfiguration();
    }
}






