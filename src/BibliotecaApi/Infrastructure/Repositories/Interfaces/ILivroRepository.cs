using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Infrastructure.Repositories.Interfaces
{
    public interface ILivroRepository
    {
        Task<int> Cadastrar(LivroEntity livro);
        Task MarcarComoIndisponivel(int idLivro);
        Task MarcarComoDisponivel(int idLivro);
    }
}