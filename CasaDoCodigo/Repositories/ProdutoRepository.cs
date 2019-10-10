using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public BuscaDeProdutosViewModel GetProdutos()
        {
            BuscaDeProdutosViewModel buscaDeProdutosViewModel = new BuscaDeProdutosViewModel();
            buscaDeProdutosViewModel.Produtos = dbSet.Include(p => p.Categoria).ToList();
            return buscaDeProdutosViewModel;
        }

        public BuscaDeProdutosViewModel GetProdutos(string pesquisa)
        {
            BuscaDeProdutosViewModel buscaDeProdutosViewModel = new BuscaDeProdutosViewModel();
            buscaDeProdutosViewModel.Produtos = dbSet
                .Where(p => p.Nome.Contains(pesquisa) || p.Categoria.NomeCategoria.Contains(pesquisa))
                .Include(p => p.Categoria)
                .ToList();

            return buscaDeProdutosViewModel;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    await categoriaRepository.SaveCategoria(livro.Categoria);
                    var categoria = categoriaRepository.GetCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
