using Microsoft.AspNetCore.Mvc;
using ProdutoApi.Models;
using ProdutoApi.Data;

namespace ProdutoApi.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoRepository _repo;

    public ProdutosController(ProdutoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<List<Produto>> GetTodos() => _repo.GetTodos();

    [HttpGet("{id}")]
    public ActionResult<Produto> GetPorId(int id)
    {
        var produto = _repo.GetPorId(id);
        if (produto == null) return NotFound();
        return produto;
    }

    [HttpPost]
    public ActionResult<Produto> Criar(Produto produto)
    {
        var novo = _repo.Adicionar(produto);
        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, novo);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Produto produto)
    {
        if (!_repo.Atualizar(id, produto)) return NotFound();

        var produtoAtualizado = _repo.GetPorId(id);
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        if (!_repo.Remover(id)) return NotFound();
        return NoContent();
    }
}
