using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Configuration.Constants;

namespace WebApplication1.Configuration
{
    public class ApiConfiguration
    {
        public string IdentityServerBaseUrl { get; set; } = AuthorizationConsts.IdentityServerBaseUrl;
        public string OidcSwaggerUIClientId { get; set; } = AuthorizationConsts.OidcSwaggerUIClientId;
        public string OidcSwaggerUIClientSecret { get; set; } = AuthorizationConsts.OidcSwaggerUIClientSecret;
        public string OidcApiName { get; set; } = AuthorizationConsts.OidcApiName;
    }
}
