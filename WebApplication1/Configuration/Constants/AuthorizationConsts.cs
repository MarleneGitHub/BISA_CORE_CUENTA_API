using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Configuration.Constants
{
    public class AuthorizationConsts
    {

        public const string IdentityServerBaseUrl = "http://localhost:5000";
        public const string OidcSwaggerUIClientId = "core_cuentas_api";
        public const string OidcSwaggerUIClientSecret = "591bfe84-6e8d-5ae5-2c05-24b35a19f380";
        public const string OidcApiName = "core_cuentas_api";
        public const string AdministrationPolicy = "RequireAdministratorRole";
        public const string AdministrationRole = "RootRole";
    }
}
