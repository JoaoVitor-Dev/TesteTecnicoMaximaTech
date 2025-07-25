using MaximaTechProductAPI.Application.Dtos;
using MaximaTechProductAPI.Core.Entities;
using MaximaTechProductAPI.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTechProductAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var departamentos = await _departamentoRepository.obterTodos();

            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obter(Guid id)
        {
            var departamento = await _departamentoRepository.obter(id);
            if (departamento == null)
                return NotFound();

            return Ok(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Departamento departamento)
        {
            var add = new Departamento
            {
                Id = Guid.NewGuid(),
                Codigo = departamento.Codigo,
                Descricao = departamento.Descricao,
                Status = true
            };

            await _departamentoRepository.Adicionar(departamento);

            return CreatedAtAction(nameof(obter), new { id = departamento.Id }, departamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Departamento departamento)
        {
            var existing = await _departamentoRepository.obter(id);
            if (existing == null)
                return NotFound();

            existing.Codigo = departamento.Codigo;
            existing.Descricao = departamento.Descricao;

            await _departamentoRepository.Atualizar(existing);

            return Ok(departamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var existing = await _departamentoRepository.obter(id);
            if (existing == null)
                return NotFound();

            await _departamentoRepository.Inativar(id);
            return Ok("Departamento inativado com sucesso");
        }
    }
}
