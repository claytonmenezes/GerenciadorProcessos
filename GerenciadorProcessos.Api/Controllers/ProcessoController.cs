using GerenciadorProcessos.Api.Controllers.Geral;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers
{
    public class ProcessoController : BaseController<Processo, RepositorioProcesso>
    {
        [HttpGet]
        [Authorize()]
        public async Task<IEnumerable> ListarAtualizar()
        {
            return new RepositorioProcesso() { usuarioId = await PegaUsuario() }.ListarAtualizar();
        }
        [HttpGet]
        [Authorize()]
        public IEnumerable ListarAtualizados()
        {
            return new RepositorioProcesso().Listar(p => p.NUP != null);
        }
    }
}
