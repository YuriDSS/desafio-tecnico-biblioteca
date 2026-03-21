using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Emprestimo.DTO;

namespace BibliotecaApi.UseCases.Emprestimo
{
    public class DevolverEmprestimoUC
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public DevolverEmprestimoUC(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public async Task<int> Executar(DevolverEmprestimoInputDTO input)
        {
            EmprestimoEntity? emprestimo = await _emprestimoRepository.ObterPorIdAsync(input.IdEmprestimo);

            bool emprestimoInvalido = emprestimo == null;
            if (emprestimoInvalido)
            {
                throw new ArgumentException("Empréstimo não encontrado.");
            }

            bool devolucaoJaFoiRealizada = emprestimo.DataDevolucao.HasValue;
            if (devolucaoJaFoiRealizada)
            {
                throw new ArgumentException("Este empréstimo já foi devolvido.");
            }

            emprestimo.RegistrarDevolucao();

            await _emprestimoRepository.Atualizar(emprestimo);
            await _livroRepository.MarcarComoDisponivel(emprestimo.IdLivro);

            return emprestimo.Id;
        }
    }
}