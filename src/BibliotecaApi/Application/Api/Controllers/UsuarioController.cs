using BibliotecaApi.Application.Api.Responses;
using BibliotecaApi.UseCases.Usuario;
using BibliotecaApi.UseCases.Usuario.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly CadastrarUsuarioUC _cadastrarUsuarioUC;

        public UsuarioController(CadastrarUsuarioUC cadastrarUsuarioUC)
        {
            _cadastrarUsuarioUC = cadastrarUsuarioUC;
        }
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioInputDTO input)
        {
            try
            {
                int newId = await _cadastrarUsuarioUC.Executar(input);
                return Ok(ApiResponse<int>.Ok(newId));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<int>.Falha("Erro ao cadastrar usuário: " + ex.Message));
            }
        }
    }
}