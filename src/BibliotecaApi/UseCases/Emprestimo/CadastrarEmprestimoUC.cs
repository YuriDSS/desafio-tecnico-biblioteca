using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Emprestimo.DTO;

namespace BibliotecaApi.UseCases.Emprestimo
{
    public class CadastrarEmprestimoUC
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarEmprestimoUC(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository, IUsuarioRepository usuarioRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> Executar(CadastrarEmprestimoInputDTO input)
        {
            bool usuarioPossuiEmprestimoEmAtraso = await _emprestimoRepository.UsuarioPossuiEmprestimoEmAtrasoAsync(input.IdUsuario);

            await _usuarioRepository.AtualizarPossuiAtrasoAtivoAsync(input.IdUsuario, usuarioPossuiEmprestimoEmAtraso);

            if (usuarioPossuiEmprestimoEmAtraso)
            {
                throw new ArgumentException("Usuário com empréstimo em atraso não pode realizar novo empréstimo.");
            }

            bool livroJaEstaEmprestado = await _emprestimoRepository.LivroEstaEmprestadoAsync(input.IdLivro);
            if (livroJaEstaEmprestado)
            {
                throw new ArgumentException("Este livro já está emprestado e ainda não foi devolvido.");
            }

            EmprestimoEntity emprestimo = new();

            emprestimo.Cadastrar(input.IdUsuario, input.IdLivro, input.DataPrevistaDevolucao);

            int idEmprestimo = await _emprestimoRepository.Cadastrar(emprestimo);

            await _livroRepository.MarcarComoIndisponivel(input.IdLivro);

            return idEmprestimo;
        }
    }
}