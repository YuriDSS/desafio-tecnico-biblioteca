using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> Cadastrar(UsuarioEntity usuario);
        Task<bool> ExisteCpfAsync(string cpf);
        Task AtualizarPossuiAtrasoAtivoAsync(int idUsuario, bool possuiAtrasoAtivo);
    }
}