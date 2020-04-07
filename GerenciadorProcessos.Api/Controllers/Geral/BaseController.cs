using GerenciadorProcessos.Domain.Entidades.Geral;
using GerenciadorProcessos.Infra.Repositorios.Geral;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerenciadorProcessos.Api.Controllers.Geral
{
    public abstract class BaseController<T, R> : DefaultApiController where T : EntidadeBase, new() where R : RepositorioPadrao<T>, new()
    {
        // GET: api/Chamado/Listar
        [Authorize()]
        [HttpGet]
        public async Task<IEnumerable<T>> Listar()
        {
            R repo = new R();
            repo.usuarioId = await PegaUsuario();
            return repo.Listar();
        }
        //GET: api/Chamado/Buscar/5
        [Authorize()]
        [HttpGet]
        public async Task<IHttpActionResult> Buscar(int id)
        {
            if (id == -1)
            {
                T obj = new T();
                return Ok(obj);
            }
            else
            {
                R repo = new R();
                repo.usuarioId = await PegaUsuario();
                T obj = repo.Buscar(id);

                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
        }
        // PUT: api/Chamado/Alterar/5
        [Authorize()]
        [HttpPut]
        public async Task<IHttpActionResult> Alterar(T obj)
        {
            PreAlterar(obj);

            R repo = new R();
            //repo.usuarioId = await PegaUsuario();

            repo.Alterar(obj);

            try
            {
                repo.Salvar();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Exists(obj.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            obj = repo.Buscar(obj.Id);
            return CreatedAtRoute("DefaultApi", new { id = obj.Id }, obj);
        }
        // POST: api/Chamado/Incluir
        [Authorize()]
        [HttpPost]
        public async Task<IHttpActionResult> Incluir()
        {
            string json = Request.Content.ReadAsStringAsync().Result;
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            PreIncluir(obj);
            R repo = new R();
            repo.usuarioId = await PegaUsuario();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Incluir(obj);
            repo.Salvar();
            obj = repo.Buscar(obj.Id);
            return CreatedAtRoute("DefaultApi", new { id = obj.Id }, obj);
        }
        // DELETE: api/Chamado/Excluir/5
        [Authorize()]
        [HttpDelete]
        public async Task<IHttpActionResult> Excluir(int id)
        {
            R repo = new R();
            repo.usuarioId = await PegaUsuario();

            T obj = repo.Buscar(id);
            if (obj == null)
            {
                return NotFound();
            }

            repo.Excluir(obj);
            repo.Salvar();

            return Ok(obj);
        }
        private async Task<bool> Exists(int id)
        {
            R repo = new R();
            repo.usuarioId = await PegaUsuario();

            return repo.Listar(e => e.Id == id).ToList().Count > 0;
        }
        protected virtual void PreIncluir(T obj)
        {
        }

        protected virtual void PreAlterar(T obj)
        {
        }
    }
}
