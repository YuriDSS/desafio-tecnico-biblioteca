using BibliotecaApi.Application.Api.Responses;
using BibliotecaApi.UseCases.Emprestimo;
using BibliotecaApi.UseCases.Emprestimo.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly CadastrarEmprestimoUC _cadastrarEmprestimoUC;
        private readonly DevolverEmprestimoUC _devolverEmprestimoUC;

        public EmprestimoController(CadastrarEmprestimoUC cadastrarEmprestimoUC, DevolverEmprestimoUC devolverEmprestimoUC)
        {
            _cadastrarEmprestimoUC = cadastrarEmprestimoUC;
            _devolverEmprestimoUC = devolverEmprestimoUC;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarEmprestimoInputDTO input)
        {
            try
            {
                int idEmprestimo = await _cadastrarEmprestimoUC.Executar(input);
                return Ok(ApiResponse<int>.Ok(idEmprestimo));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<int>.Falha("Erro ao registrar empréstimo: " + ex.Message));
            }
        }

        [HttpPost("Devolver")]
        public async Task<IActionResult> Devolver([FromBody] DevolverEmprestimoInputDTO input)
        {
            try
            {
                int idEmprestimo = await _devolverEmprestimoUC.Executar(input);
                return Ok(ApiResponse<int>.Ok(idEmprestimo));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<int>.Falha("Erro ao devolver empréstimo: " + ex.Message));
            }
        }
    }
}