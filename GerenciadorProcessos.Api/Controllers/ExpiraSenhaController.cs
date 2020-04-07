using GerenciadorProcessos.Infra.Repositorios;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers
{
    public class ExpiraSenhaController : ApiController
    {
        [HttpPost]
        public IHttpActionResult RedefineSenha(string email, string senha)
        {
            var repoUsuario = new RepositorioUsuario();
            var usuario = repoUsuario.RedefineSenha(email, senha);
            if (!usuario.ExpiraSenha)
            {
                return Ok(new { email = email, senha = senha });
            }
            else
            {
                return BadRequest("A senha não foi redefinida");
            }
        }
    }
}
