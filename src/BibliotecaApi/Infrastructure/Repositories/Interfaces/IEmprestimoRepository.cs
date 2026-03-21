using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Infrastructure.Repositories.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<int> Cadastrar(EmprestimoEntity emprestimo);
        Task Atualizar(EmprestimoEntity emprestimo);
        Task<EmprestimoEntity?> ObterPorIdAsync(int idEmprestimo);
    }
}