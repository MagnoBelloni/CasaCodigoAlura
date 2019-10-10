using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task SaveCategoria(string nomeCategoria);
        IList<Categoria> GetCategorias();
        Categoria GetCategoria(string nomeCategoria);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }        

        public IList<Categoria> GetCategorias()
        {
            return dbSet.ToList();
        }

        public async Task SaveCategoria(string nomeCategoria)
        {
            if (!dbSet.Where(p => p.NomeCategoria == nomeCategoria).Any())
            {
                dbSet.Add(new Categoria(nomeCategoria));
            }
            await contexto.SaveChangesAsync();
        }

        public Categoria GetCategoria(string nomeCategoria)
        {
            return dbSet.Where(c => c.NomeCategoria == nomeCategoria).SingleOrDefault();
        }        
    }    
}
