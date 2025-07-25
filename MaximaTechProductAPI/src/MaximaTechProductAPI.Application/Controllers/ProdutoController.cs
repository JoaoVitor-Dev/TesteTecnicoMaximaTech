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
            var lista = produtos.Select(p => new ProdutoDto
            {
                Codigo = p.Codigo,
                Descricao = p.Descricao,
                DepartamentoId = p.DepartamentoId,
                Preco = p.Preco
            });

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var produto = await _produtoRepository.obter(id);
            if (produto == null) return NotFound();

            var dto = new ProdutoDto
            {
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                DepartamentoId = produto.DepartamentoId,
                Preco = produto.Preco
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] ProdutoDto dto)
        {
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Codigo = dto.Codigo,
                Descricao = dto.Descricao,
                DepartamentoId = dto.DepartamentoId,
                Preco = dto.Preco,
                Status = true
            };

            await _produtoRepository.Adicionar(produto);
            return CreatedAtAction(nameof(Obter), new { id = produto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProdutoDto dto)
        {
            var existente = await _produtoRepository.obter(id);
            if (existente == null) return NotFound();

            existente.Codigo = dto.Codigo;
            existente.Descricao = dto.Descricao;
            existente.DepartamentoId = dto.DepartamentoId;
            existente.Preco = dto.Preco;

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
