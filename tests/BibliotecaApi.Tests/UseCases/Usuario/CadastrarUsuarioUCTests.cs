using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.Tests.Fixtures;
using BibliotecaApi.UseCases.Usuario;
using BibliotecaApi.UseCases.Usuario.DTO;
using Moq;

namespace BibliotecaApi.Tests.UseCases.Usuario
{
    public class CadastrarUsuarioUCTests
    {
        private readonly UsuarioFixture _fixture = new();

        [Fact]
        public async Task Deve_Cadastrar_Usuario_Quando_Cpf_Nao_Existir()
        {
            Mock<IUsuarioRepository> usuarioRepositoryMock = new(MockBehavior.Strict);
            CadastrarUsuarioInputDTO input = _fixture.CriarInputValido();

            usuarioRepositoryMock.Setup(x => x.ExisteCpfAsync(It.IsAny<string>())).ReturnsAsync(false);
            usuarioRepositoryMock.Setup(x => x.Cadastrar(It.IsAny<UsuarioEntity>())).ReturnsAsync(1);

            CadastrarUsuarioUC useCase = new(usuarioRepositoryMock.Object);
            int idUsuario = await useCase.Executar(input);
            Assert.Equal(1, idUsuario);

            usuarioRepositoryMock.Verify(x => x.ExisteCpfAsync(It.Is<string>(cpf => cpf == _fixture.CpfNormalizadoValido)), Times.Once);
            usuarioRepositoryMock.Verify(x => x.Cadastrar(It.Is<UsuarioEntity>(u => u.Nome == _fixture.NomeValido && u.CPF == _fixture.CpfNormalizadoValido && u.Email == _fixture.EmailValido)), Times.Once);
            usuarioRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Cpf_Ja_Existir()
        {
            Mock<IUsuarioRepository> usuarioRepositoryMock = new(MockBehavior.Strict);
            CadastrarUsuarioInputDTO input = _fixture.CriarInputValido();

            usuarioRepositoryMock.Setup(x => x.ExisteCpfAsync(It.IsAny<string>())).ReturnsAsync(true);

            CadastrarUsuarioUC useCase = new(usuarioRepositoryMock.Object);
            ArgumentException excecao = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Executar(input));
            Assert.Equal("Usuário com este CPF já está cadastrado.", excecao.Message);

            usuarioRepositoryMock.Verify(x => x.ExisteCpfAsync(It.Is<string>(cpf => cpf == _fixture.CpfNormalizadoValido)), Times.Once);
            usuarioRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<UsuarioEntity>()), Times.Never);
            usuarioRepositoryMock.VerifyNoOtherCalls();
        }
    }
}