using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Livro.DTO;

namespace BibliotecaApi.UseCases.Livro
{
    public class CadastrarLivroUC
    {
        private readonly ILivroRepository _livroRepository;

        public CadastrarLivroUC(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<int> Executar(CadastrarLivroInputDTO input)
        {
            var livro = new LivroEntity();
            livro.Cadastrar(input.Titulo, input.Autor, input.ISBN);

            return await _livroRepository.Cadastrar(livro);
        }
    }
}