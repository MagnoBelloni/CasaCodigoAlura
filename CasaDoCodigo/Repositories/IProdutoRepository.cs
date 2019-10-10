using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        Task SaveProdutos(List<Livro> livros);
        BuscaDeProdutosViewModel GetProdutos();
        BuscaDeProdutosViewModel GetProdutos(string pesquisa);
    }
}