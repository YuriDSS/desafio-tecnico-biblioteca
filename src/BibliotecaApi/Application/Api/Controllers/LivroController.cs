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
        private readonly ListarLivrosUC _listarLivrosUC;

        public LivroController(CadastrarLivroUC cadastrarLivroUC, ListarLivrosUC listarLivrosUC)
        {
            _cadastrarLivroUC = cadastrarLivroUC;
            _listarLivrosUC = listarLivrosUC;
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

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<ListarLivrosOutputDTO> livros = await _listarLivrosUC.Executar();
                return Ok(ApiResponse<List<ListarLivrosOutputDTO>>.Ok(livros));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<ListarLivrosOutputDTO>>.Falha(ex.Message));
            }
        }
    }
}