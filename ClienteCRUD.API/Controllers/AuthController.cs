using ClienteCRUD.Application.DTOs;
using ClienteCRUD.Application.Services;
using ClienteCRUD.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public AuthController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Autenticar cliente
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var isValid = await _clienteService.ValidarLoginAsync(loginDTO.Usuario, loginDTO.Senha);

                if (!isValid)
                    return Unauthorized("Usuário ou senha inválidos");

                return Ok(new { message = "Login realizado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
} 