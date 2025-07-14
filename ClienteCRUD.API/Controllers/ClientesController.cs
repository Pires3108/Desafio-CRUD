using AutoMapper;
using ClienteCRUD.Application.DTOs;
using ClienteCRUD.Core.Entities;
using ClienteCRUD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(ClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> ObterTodos()
        {
            try
            {
                var clientes = await _clienteService.ObterTodosAsync();
                var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
                return Ok(clientesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obter funcionários com permissões baseadas no funcionário logado
        /// </summary>
        [HttpGet("com-permissoes/{funcionarioId}")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> ObterComPermissoes(int funcionarioId)
        {
            try
            {
                var funcionarios = await _clienteService.ObterTodosComPermissoesAsync(funcionarioId);
                var funcionariosDTO = _mapper.Map<IEnumerable<ClienteDTO>>(funcionarios);
                return Ok(funcionariosDTO);
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
        /// Obter cliente por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> ObterPorId(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterPorIdAsync(id);
                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
                return Ok(clienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Criar novo cliente
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Criar([FromBody] CriarClienteDTO criarClienteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clienteCriado = await _clienteService.CriarAsync(criarClienteDTO);
                var clienteDTO = _mapper.Map<ClienteDTO>(clienteCriado);

                return CreatedAtAction(nameof(ObterPorId), new { id = clienteDTO.Id }, clienteDTO);
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
        /// Atualizar cliente (sem verificação de permissões - para uso interno)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> Atualizar(int id, [FromBody] AtualizarClienteDTO atualizarClienteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = _mapper.Map<Cliente>(atualizarClienteDTO);
                var clienteAtualizado = await _clienteService.AtualizarAsync(id, cliente);
                var clienteDTO = _mapper.Map<ClienteDTO>(clienteAtualizado);

                return Ok(clienteDTO);
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
        /// Atualizar cliente com verificação de permissões
        /// </summary>
        [HttpPut("{id}/com-permissoes")]
        public async Task<ActionResult<ClienteDTO>> AtualizarComPermissoes(int id, [FromBody] AtualizarClienteDTO atualizarClienteDTO, [FromQuery] int funcionarioLogadoId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = _mapper.Map<Cliente>(atualizarClienteDTO);
                var clienteAtualizado = await _clienteService.AtualizarComPermissoesAsync(id, cliente, funcionarioLogadoId);
                var clienteDTO = _mapper.Map<ClienteDTO>(clienteAtualizado);

                return Ok(clienteDTO);
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
        /// Excluir cliente (sem verificação de permissões - para uso interno)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var resultado = await _clienteService.ExcluirAsync(id);
                if (!resultado)
                    return NotFound("Cliente não encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Excluir cliente com verificação de permissões
        /// </summary>
        [HttpDelete("{id}/com-permissoes")]
        public async Task<ActionResult> ExcluirComPermissoes(int id, [FromQuery] int funcionarioLogadoId)
        {
            try
            {
                var resultado = await _clienteService.ExcluirComPermissoesAsync(id, funcionarioLogadoId);
                if (!resultado)
                    return NotFound("Cliente não encontrado");

                return NoContent();
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
        /// Reativar cliente inativo
        /// </summary>
        [HttpPost("{id}/reativar")]
        public async Task<ActionResult<ClienteDTO>> Reativar(int id)
        {
            try
            {
                var cliente = await _clienteService.ReativarAsync(id);
                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
                return Ok(clienteDTO);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Login de funcionário (aceita usuário OU email)
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ClienteDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Buscar por usuário OU email
                var funcionarios = await _clienteService.ObterTodosAsync();
                var funcionario = funcionarios.FirstOrDefault(f =>
                    (f.Usuario == loginDTO.Usuario || f.Email == loginDTO.Usuario) &&
                    f.Ativo);

                if (funcionario == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Senha, funcionario.Senha))
                    return Unauthorized("Usuário ou senha inválidos");

                var funcionarioDTO = _mapper.Map<ClienteDTO>(funcionario);
                return Ok(funcionarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
} 