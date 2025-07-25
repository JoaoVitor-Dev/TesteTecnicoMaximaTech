using MaximaTechProductAPI.Application.Dtos;
using MaximaTechProductAPI.Core.Entities;
using MaximaTechProductAPI.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTechProductAPI.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var produtos = await _produtoRepository.obterTodos();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var produto = await _produtoRepository.obter(id);
            
            if (produto == null) return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Produto produto)
        {
            var add = new Produto
            {
                Id = Guid.NewGuid(),
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                DepartamentoId = produto.DepartamentoId,
                Preco = produto.Preco,
                Status = true
            };

            await _produtoRepository.Adicionar(produto);
            return CreatedAtAction(nameof(Obter), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Produto produto)
        {
            var existente = await _produtoRepository.obter(id);
            if (existente == null) return NotFound();

            existente.Codigo = produto.Codigo;
            existente.Descricao = produto.Descricao;
            existente.DepartamentoId = produto.DepartamentoId;
            existente.Preco = produto.Preco;

            await _produtoRepository.Atualizar(existente);
            return Ok(existente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existente = await _produtoRepository.obter(id);
            if (existente == null) return NotFound();

            await _produtoRepository.Inativar(id);
            return Ok("Produto inativado com sucesso");
        }

    }
}
