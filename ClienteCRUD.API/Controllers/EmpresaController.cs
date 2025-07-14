using AutoMapper;
using ClienteCRUD.Core.Entities;
using ClienteCRUD.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipesController : ControllerBase
    {
        private readonly IEquipeService _equipeService;
        private readonly IMapper _mapper;

        public EquipesController(IEquipeService equipeService, IMapper mapper)
        {
            _equipeService = equipeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todas as equipes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipe>>> ObterTodas()
        {
            try
            {
                var equipes = await _equipeService.ObterTodasAsync();
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obter equipe por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> ObterPorId(int id)
        {
            try
            {
                var equipe = await _equipeService.ObterPorIdAsync(id);
                if (equipe == null)
                    return NotFound("Equipe não encontrada");

                return Ok(equipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Criar nova equipe
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Equipe>> Criar([FromBody] Equipe equipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var equipeCriada = await _equipeService.CriarAsync(equipe);
                return CreatedAtAction(nameof(ObterPorId), new { id = equipeCriada.Id }, equipeCriada);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualizar equipe
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Equipe>> Atualizar(int id, [FromBody] Equipe equipe)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var equipeAtualizada = await _equipeService.AtualizarAsync(id, equipe);
                return Ok(equipeAtualizada);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Excluir equipe
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var resultado = await _equipeService.ExcluirAsync(id);
                if (!resultado)
                    return NotFound("Equipe não encontrada");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
} 