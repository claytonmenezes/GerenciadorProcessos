using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers.Geral
{
    public class ServerOnlineController : ApiController
    {
        [HttpGet]
        public int Ping()
        {
            return 1;
        }
    }
}
