using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Livro.DTO;

namespace BibliotecaApi.UseCases.Livro
{
    public class ListarLivrosUC
    {
        private readonly ILivroRepository _livroRepository;

        public ListarLivrosUC(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<List<ListarLivrosOutputDTO>> Executar()
        {
            return await _livroRepository.ListarAsync();
        }
    }
}