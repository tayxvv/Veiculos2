using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using veiculos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace veiculos.Controllers;

public class PessoasController : Controller
{
    private readonly ILogger<Controller> _logger;

    public PessoasController(ILogger<PessoasController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Pessoa(int id)
    {
        Repositorio<Pessoa> repo = new Repositorio<Pessoa>();
        Pessoa Pessoa = repo.Buscar(id);

        Repositorio<Produto> repoProduto = new Repositorio<Produto>();
        ViewBag.Produto = new SelectList(repoProduto.Listar(), "Id", "Descricao");

        Repositorio<Veiculo> repoVeiculo = new Repositorio<Veiculo>();
        ViewBag.Veiculo = new SelectList(repoVeiculo.Listar(), "Id", "Nome");

        return View(Pessoa);
    }

    public IActionResult Pessoa()
    {
        Repositorio<Produto> repoProduto = new Repositorio<Produto>();
        ViewBag.Produto = new SelectList(repoProduto.Listar(), "Id", "Descricao");

        Repositorio<Veiculo> repoVeiculo = new Repositorio<Veiculo>();
        ViewBag.Veiculo = new SelectList(repoVeiculo.Listar(), "Id", "Nome");
        return View();
    }

    public IActionResult Listar()
    {
        Repositorio<Pessoa> repo = new Repositorio<Pessoa>();
        List<Pessoa> lista = repo.Listar();
        return View(lista);
    }

    [HttpPost]
    public IActionResult Pessoa(Pessoa model)
    {   
        if(!ModelState.IsValid) {
            Repositorio<Produto> repoProduto = new Repositorio<Produto>();
            ViewBag.Produto = new SelectList(repoProduto.Listar(), "Id", "Descricao", model.ProdutosId);

            Repositorio<Veiculo> repoVeiculo = new Repositorio<Veiculo>();
            ViewBag.Veiculo = new SelectList(repoVeiculo.Listar(), "Id", "Nome", model.VeiculosId);

            return View(model);
        }
        Repositorio<Pessoa> repo = new Repositorio<Pessoa>();
        if (model.Id != null && model.Id != 0) {
            repo.Atualizar(model);
        } else {
            repo.Adicionar(model);
        }
        return RedirectToAction("Listar");
    }

    public IActionResult Apagar(int id)
    {
        Repositorio<Pessoa> repo = new Repositorio<Pessoa>();
        Pessoa pessoa = repo.Buscar(id);
        
        if (pessoa == null)
        {
            return NotFound();
        }

        return View(pessoa);
    }

    [HttpPost]
    public IActionResult Apagar(Pessoa pessoa)
    {
        Repositorio<Pessoa> repo = new Repositorio<Pessoa>();
        repo.Remover(pessoa.Id);
        return RedirectToAction("Listar");
    }
}
