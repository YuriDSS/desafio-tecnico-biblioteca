using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Livro;
using BibliotecaApi.UseCases.Livro.DTO;
using Moq;

namespace BibliotecaApi.Tests.UseCases.Livro
{
    public class ListarLivrosUCTests
    {
        [Fact]
        public async Task Executar_DeveRetornarListaDeLivros()
        {
            Mock<ILivroRepository> livroRepositoryMock = new();

            List<ListarLivrosOutputDTO> livrosEsperados =
            [
                new ListarLivrosOutputDTO
                {
                    Id = 1,
                    Titulo = "Star Wars: Herdeiro do Império",
                    Autor = "Timothy Zahn",
                    ISBN = "9788576571056"
                }
            ];

            livroRepositoryMock.Setup(x => x.ListarAsync()).ReturnsAsync(livrosEsperados);

            ListarLivrosUC useCase = new(livroRepositoryMock.Object);

            List<ListarLivrosOutputDTO> resultado = await useCase.Executar();

            Assert.Single(resultado);
            Assert.Equal("Star Wars: Herdeiro do Império", resultado[0].Titulo);
            Assert.Equal("Timothy Zahn", resultado[0].Autor);
            Assert.Equal("9788576571056", resultado[0].ISBN);
        }
    }
}