using GerenciadorProcessos.Api.Controllers.Geral;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios;
using System.Linq;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers
{
    public class TipoEventoController : BaseController<TipoEvento, RepositorioTipoEvento>
    {
        [HttpGet]
        [Authorize()]
        public IHttpActionResult BuscarPorCodEvento(int codigo)
        {
            var retorno = new RepositorioTipoEvento().Listar(te => te.CodEvento == codigo).FirstOrDefault();
            return Ok(retorno);
        }
    }
}
