using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios.Geral;
using System.Linq;

namespace GerenciadorProcessos.Infra.Repositorios
{
    public class RepositorioUsuario : RepositorioPadrao<Usuario>
    {
        public Usuario RedefineSenha(string email, string senha)
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario.ExpiraSenha)
            {
                usuario.Senha = senha.ToString();
                usuario.ExpiraSenha = false;
                db.Entry<Usuario>(usuario);
                db.SaveChanges();
            }
            else
            {
                usuario.Senha = null;
                usuario.ExpiraSenha = true;
            }
            return usuario;
        }
    }
}
