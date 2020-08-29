using System.Collections.Generic;
using FooBlog.Admin.Configuration.Identity;

namespace FooBlog.Admin.Configuration.IdentityServer
{
    public class Client : global::IdentityServer4.Models.Client
    {
        public List<Claim> ClientClaims { get; set; } = new List<Claim>();
    }
}






