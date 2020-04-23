using GerenciadorProcessos.Api.Controllers.Geral;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers
{
    public class ParametroUsuarioController : BaseController<ParametroUsuario, RepositorioParametroUsuario>
    {
        [Authorize()]
        [HttpGet]
        public async Task<IHttpActionResult> BuscarParametro()
        {
            return Ok(new RepositorioParametroUsuario() { usuarioId = await PegaUsuario() }.BuscarParametro());
        }
    }
}
