using ProdutoApi.Models;

namespace ProdutoApi.Data;

public class ProdutoRepository
{
    private static List<Produto> _produtos = new List<Produto>();
    private static int _proximoId = 1;

    public List<Produto> GetTodos() => _produtos;

    public Produto? GetPorId(int id) => _produtos.FirstOrDefault(p => p.Id == id);

    public Produto Adicionar(Produto produto)
    {
        produto.Id = _proximoId++;
        _produtos.Add(produto);
        return produto;
    }

    public bool Atualizar(int id, Produto produto)
    {
        var existente = GetPorId(id);
        if (existente == null) return false;

        existente.Nome = produto.Nome;
        existente.Preco = produto.Preco;
        existente.Quantidade = produto.Quantidade;
        return true;
    }

    public bool Remover(int id)
    {
        var produto = GetPorId(id);
        if (produto == null) return false;

        _produtos.Remove(produto);
        return true;
    }
}
