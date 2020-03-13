using GerenciadorProcessos.Domain.Entidades.Geral;
using GerenciadorProcessos.Infra.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorProcessos.Infra.Repositorios.Geral
{
    public abstract class RepositorioPadrao<T> where T : EntidadeBase
    {
        public int? usuarioId { get; set; }
        protected Contexto.Contexto db = new Contexto.Contexto();
        public int Salvar()
        {
            try
            {
                PreSalvar();
                int qtd = db.SaveChanges();
                PosSalvar();
                return qtd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T Incluir(T obj)
        {
            PreIncluir(obj);
            var retorno = db.Set<T>().Add(obj);
            PosIncluir(obj);
            return retorno;
        }
        public virtual T Buscar(int id)
        {
            return Listar(e => e.Id == id).FirstOrDefault();
        }
        public virtual IQueryable<T> Listar()
        {
            return db.Set<T>().AsNoTracking();
        }
        public virtual IQueryable<T> Listar(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).AsNoTracking();
        }
        public void Alterar(T obj)
        {
            if (!Existe(obj.Id)) throw new Exception("ID não encontrado");
            PreAlterar(obj);
            UtilsRepositorio.GravarDependentes(obj, db);
            db.Entry(obj).State = EntityState.Modified;
            PosAlterar(obj);
        }
        public void Excluir(T obj)
        {
            if (!Existe(obj.Id)) throw new Exception("ID não encontrado");
            PreExcluir(obj);
            UtilsRepositorio.ExcluirDependentes(obj, db);
            db.Entry<T>(obj).State = EntityState.Deleted;
            PosExcluir(obj);
        }
        public bool Existe(int id)
        {
            return db.Set<T>().Count(e => e.Id == id) > 0;
        }
        protected virtual void PreIncluir(T obj)
        {
        }
        protected virtual void PosIncluir(T obj)
        {
        }
        protected virtual void PreAlterar(T obj)
        {
        }
        protected virtual void PosAlterar(T obj)
        {
        }
        protected virtual void PreExcluir(T obj)
        {
        }
        protected virtual void PosExcluir(T obj)
        {
        }
        protected virtual void PreSalvar()
        {
        }
        protected virtual void PosSalvar()
        {

        }
    }
}
