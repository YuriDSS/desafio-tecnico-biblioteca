using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Emprestimo.DTO;

namespace BibliotecaApi.UseCases.Emprestimo
{
    public class CadastrarEmprestimoUC
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public CadastrarEmprestimoUC(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public async Task<int> Executar(CadastrarEmprestimoInputDTO input)
        {
            EmprestimoEntity emprestimo = new();

            emprestimo.Cadastrar(input.IdUsuario, input.IdLivro, input.DataPrevistaDevolucao);

            int idEmprestimo = await _emprestimoRepository.Cadastrar(emprestimo);

            await _livroRepository.MarcarComoIndisponivel(input.IdLivro);

            return idEmprestimo;
        }
    }
}