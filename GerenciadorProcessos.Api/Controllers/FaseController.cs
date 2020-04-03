using GerenciadorProcessos.Api.Controllers.Geral;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios;
using System.Linq;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers
{
    public class FaseController : BaseController<Fase, RepositorioFase>
    {
        [HttpGet]
        [Authorize()]
        public IHttpActionResult BuscarPorNome(string nome)
        {
            var retorno = new RepositorioFase().Listar(f => f.Nome.ToLower().Trim() == nome.ToLower().Trim()).FirstOrDefault();
            return Ok(retorno);
        }
    }
}
