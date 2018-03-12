using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sistema_Crud.API.Helper
{
    public class AutenicationProviderServer : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var username = context.UserName;
                var password = context.Password;

                if(username != "Tiago" && password != "123456" ){
                    context.SetError("Dados Invalidos");
                }

                var roles = new List<string>();
                roles.Add("User");



                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, username));

                GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());
                Thread.CurrentPrincipal = principal; ;

                context.Validated(identity);


            }
            catch
            {

            }

        }

    }
}