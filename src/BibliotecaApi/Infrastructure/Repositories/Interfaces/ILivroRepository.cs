using BibliotecaApi.Domain.Entities;
using BibliotecaApi.UseCases.Livro.DTO;

namespace BibliotecaApi.Infrastructure.Repositories.Interfaces
{
    public interface ILivroRepository
    {
        Task<int> Cadastrar(LivroEntity livro);
        Task MarcarComoIndisponivel(int idLivro);
        Task MarcarComoDisponivel(int idLivro);
        Task<List<ListarLivrosOutputDTO>> ListarAsync();
    }
}