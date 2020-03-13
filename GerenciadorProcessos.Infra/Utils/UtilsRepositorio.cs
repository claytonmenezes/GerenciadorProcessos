using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace GerenciadorProcessos.Infra.Utils
{
    class UtilsRepositorio
    {
        public static void GravarDependentes(object obj, Contexto.Contexto db)
        {
            bool descendenteEntidadeBase = VerificarHerancaEntidadeBase(obj);

            if (!descendenteEntidadeBase)
            {
                throw new Exception("O parâmetro fornecido deve herdar de EntidadeBase.");
            }

            var exclusaoEntidades = new List<object>();

            // Referência de tipos de todas as propriedades do obj que são dependentes e que são enumeradas (As não enumeradas são gravadas na entidade principal)
            IEnumerable<PropertyInfo> dependentesEnumeradosMetaData = ListarDependentesEnumerados(obj);

            foreach (var dependenteMetaData in dependentesEnumeradosMetaData)
            {
                IList instanciaDependente = (IList)obj.GetType().GetProperty(dependenteMetaData.Name).GetValue(obj);

                if (instanciaDependente != null && instanciaDependente.Count > 0)
                {
                    foreach (var dependente in instanciaDependente)
                    {
                        // Propriedades do dependente atual que são classes, se tiverem chaves (Foreign Keys) definidas
                        // devem ficar nulas
                        List<PropertyInfo> propsDependente = dependente.GetType().GetProperties().Where(prop => prop.PropertyType.IsClass && prop.PropertyType.Name != "String").ToList();

                        foreach (PropertyInfo ClasseEmDependente in propsDependente)
                        {
                            string NomeChaveClasse = ClasseEmDependente.Name + "Id";
                            int? valorChaveClasse = 0;
                            if (dependente.GetType().GetRuntimeProperty(NomeChaveClasse) != null)
                            {
                                valorChaveClasse = (int?)dependente.GetType().GetRuntimeProperty(NomeChaveClasse).GetValue(dependente);
                            }

                            if (valorChaveClasse != null && valorChaveClasse > 0)
                            {
                                ClasseEmDependente.SetValue(dependente, null);
                            }
                        }

                        // Fazendo associação das chaves do obj principal nos filhos

                        // Validando a chave da Classe principal no dependente atual
                        var chavePai = propsDependente.ToList().Where(prop => prop.PropertyType.IsAssignableFrom(obj.GetType())).FirstOrDefault();
                        if (chavePai != null)
                        {
                            string nome = chavePai.Name + "Id";
                            var valorChaveEntidadePai = obj.GetType().GetRuntimeProperty("Id").GetValue(obj);
                            dependente.GetType().GetProperty(nome).SetValue(dependente, valorChaveEntidadePai);
                        }

                        // Gravando as dependências de cada dependente recursivamente
                        GravarDependentes(dependente, db);

                        if (!Convert.ToBoolean(dependente.GetType().GetProperty("Excluir").GetValue(dependente)))
                        {
                            // Validar se o Id é 0
                            if (Convert.ToInt64(dependente.GetType().GetProperty("Id").GetValue(dependente)) == 0)
                            {
                                db.Entry(dependente).State = EntityState.Added;
                            }
                            else
                            {
                                // Essa validação é necessária, pois se algum dependente recursivo atualizar o principal para "alterado", 
                                // não se pode mais agendar a modificação no entity, caso contrário pode haver o conflito (Se o state da dependência já foi
                                // atualizado, não é permitido alterar mais.
                                if (db.Entry(dependente).State != EntityState.Modified)
                                {
                                    db.Entry(dependente).State = EntityState.Modified;
                                }
                            }
                        }
                        else
                        {
                            exclusaoEntidades.Add(dependente);
                        }
                    }
                }
            }

            db.Entry(obj).State = EntityState.Modified;
            foreach (var objExcl in exclusaoEntidades)
            {
                ExcluirDependentes(objExcl, db);
                db.Entry(objExcl).State = EntityState.Deleted;
            }
        }

        public static void ExcluirDependentes(object obj, Contexto.Contexto db)
        {
            bool descendenteEntidadeBase = VerificarHerancaEntidadeBase(obj);

            if (!descendenteEntidadeBase)
            {
                throw new Exception("O parâmetro fornecido deve herdar de EntidadeBase.");
            }

            IEnumerable<PropertyInfo> MetaDataDependentes = ListarDependentesEnumerados(obj);

            foreach (var dependenteMetaData in MetaDataDependentes)
            {
                // Preciso da instância do dependente, para verificar os tipos e saber se existem dependentes recursivos
                IList instanciaDependente = (IList)obj.GetType().GetProperty(dependenteMetaData.Name).GetValue(obj);

                // Bloco que faz a mudança do state para deleted do dependente atual
                if (instanciaDependente != null)
                {
                    int qtde = instanciaDependente.Count;
                    while (qtde > 0)
                    {
                        ExcluirDependentes(instanciaDependente[qtde - 1], db);
                        db.Entry(instanciaDependente[qtde - 1]).State = EntityState.Deleted;
                        --qtde;
                    }
                }
            }
        }

        /// <summary>
        /// Retorna true para parâmetro herdeiro da classe EntidadeBase.
        /// </summary>
        static bool VerificarHerancaEntidadeBase(object obj)
        {
            return typeof(EntidadeBase).IsAssignableFrom(obj.GetType());
        }

        static IEnumerable<PropertyInfo> ListarDependentesEnumerados(object obj)
        {
            return obj.GetType().GetProperties().Where(prop =>
                 typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                 prop.PropertyType.Name != "String" &&
                 typeof(EntidadeBase).IsAssignableFrom(prop.PropertyType.GenericTypeArguments[0])
            /*&& !typeof(EntidadeEmpresaBase).IsAssignableFrom(prop.PropertyType.GenericTypeArguments[0])*/
            );
        }
    }
}
