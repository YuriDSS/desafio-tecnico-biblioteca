using BibliotecaApi.Application.Api.Responses;
using BibliotecaApi.UseCases.Livro;
using BibliotecaApi.UseCases.Livro.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly CadastrarLivroUC _cadastrarLivroUC;

        public LivroController(CadastrarLivroUC cadastrarLivroUC)
        {
            _cadastrarLivroUC = cadastrarLivroUC;
        }
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarLivroInputDTO input)
        {
            try
            {
                int newId = await _cadastrarLivroUC.Executar(input);
                return Ok(ApiResponse<int>.Ok(newId));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<int>.Falha(ex.Message));
            }

        }
    }
}