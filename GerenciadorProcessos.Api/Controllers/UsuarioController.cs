using GerenciadorProcessos.Api.Controllers.Geral;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios;

namespace GerenciadorProcessos.Api.Controllers
{
    public class UsuarioController : BaseController<Usuario, RepositorioUsuario>
    {
    }
}
