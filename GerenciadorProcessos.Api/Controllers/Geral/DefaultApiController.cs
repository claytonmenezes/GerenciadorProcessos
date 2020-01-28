using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers.Geral
{
    public class DefaultApiController : ApiController
    {
        protected async Task<int> PegaUsuario()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            return await Task.Run(() => Convert.ToInt16(principal.Claims.Where(c => c.Type == "UsuarioId").Single().Value));
        }
    }
}
