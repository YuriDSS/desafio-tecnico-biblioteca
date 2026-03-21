using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Infrastructure.Repositories.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<int> Cadastrar(EmprestimoEntity emprestimo);
        Task Atualizar(EmprestimoEntity emprestimo);
        Task<EmprestimoEntity?> ObterPorIdAsync(int idEmprestimo);
        Task<bool> LivroEstaEmprestadoAsync(int idLivro);
        Task<bool> UsuarioPossuiEmprestimoEmAtrasoAsync(int idUsuario);
    }
}