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
        private readonly IDepartamentoRepository _departamentoRepository;

        public ProdutoController(IProdutoRepository produtoRepository,  IDepartamentoRepository departamentoRepository)
        {
            _produtoRepository = produtoRepository;
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodos()
        {
            var produtos = await _produtoRepository.obterTodos();

            var produtosDto = produtos.Select(p => new ProdutoDto
            {
                id = p.Id.ToString(),
                codigo = p.Codigo,
                descricao = p.Descricao,
                departamento = p.Departamento,
                preco = p.Preco
            });

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obter(Guid id)
        {
            var produto = await _produtoRepository.obter(id);
            
            if (produto == null) return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Adicionar([FromBody] Produto produto)
        {
            try
            {
                produto.Id = Guid.NewGuid();
                produto.Status = true;
                
                await _produtoRepository.Adicionar(produto);
                return CreatedAtAction(nameof(Obter), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Produto produto)
        {
            var existente = await _produtoRepository.obter(id);
            if (existente == null)
                return NotFound();
            
            var departamento = await _departamentoRepository.obter(produto.DepartamentoId);
            if (departamento == null)
                return BadRequest("Departamento informado não identificado");
            
            existente.Codigo = produto.Codigo;
            existente.Descricao = produto.Descricao;
            existente.DepartamentoId = produto.DepartamentoId;
            existente.Preco = produto.Preco;

            await _produtoRepository.Atualizar(existente);
            return Ok(existente);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var existente = await _produtoRepository.obter(id);
            if (existente == null) return NotFound();

            await _produtoRepository.Inativar(id);
            return Ok("Produto inativado com sucesso");
        }

    }
}
