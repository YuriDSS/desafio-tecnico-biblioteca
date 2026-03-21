using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Emprestimo.DTO;

namespace BibliotecaApi.UseCases.Emprestimo
{
    public class DevolverEmprestimoUC
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DevolverEmprestimoUC(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository, IUsuarioRepository usuarioRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
            _usuarioRepository = usuarioRepository;
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

            bool usuarioAindaPossuiEmprestimoEmAtraso = await _emprestimoRepository.UsuarioPossuiEmprestimoEmAtrasoAsync(emprestimo.IdUsuario);
            await _usuarioRepository.AtualizarPossuiAtrasoAtivoAsync(emprestimo.IdUsuario, usuarioAindaPossuiEmprestimoEmAtraso);

            return emprestimo.Id;
        }
    }
}