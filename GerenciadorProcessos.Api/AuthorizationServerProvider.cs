using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Contexto;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GerenciadorProcessos.Api
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                try
                {
                    var user = context.UserName;
                    var password = context.Password;

                    var db = new Contexto();
                    var usuarios = db.Usuarios.Where(u => u.Email == user).ToList();
                    Usuario usuario = null;
                    if (usuarios.Count == 1)
                        usuario = usuarios[0];

                    if (usuario.ExpiraSenha)
                    {
                        context.SetError("invalid_grant", "Senha Expirada", "ExpiraSenha");
                        return;
                    }

                    if (usuarios.Count != 1 || password != usuario.Senha || !usuario.Ativo)
                    {
                        context.SetError("invalid_grant", "Usuário ou senha inválidos");
                        return;
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    identity.AddClaim(new Claim("UsuarioId", usuario.Id.ToString()));

                    context.Validated(identity);
                }
                catch (Exception e)
                {
                    context.SetError("invalid_grant", new JavaScriptSerializer().Serialize(new { message = "Falha ao autenticar " + e.Message }));
                }
            });
        }
    }
}