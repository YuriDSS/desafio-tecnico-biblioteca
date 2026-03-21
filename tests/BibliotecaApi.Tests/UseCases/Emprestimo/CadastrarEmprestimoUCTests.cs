using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.Domain.Entities;
using BibliotecaApi.UseCases.Emprestimo;
using BibliotecaApi.UseCases.Emprestimo.DTO;
using Moq;

namespace BibliotecaApi.Tests.UseCases.Emprestimo
{
    public class CadastrarEmprestimoUCTests
    {
        [Fact]
        public async Task Executar_QuandoLivroJaEstaEmprestado_DeveLancarExcecao()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            emprestimoRepositoryMock
                .Setup(x => x.LivroEstaEmprestadoAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            CadastrarEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            CadastrarEmprestimoInputDTO input = new()
            {
                IdUsuario = 1,
                IdLivro = 1,
                DataPrevistaDevolucao = DateTime.Now.AddDays(3)
            };

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Executar(input));

            Assert.Equal("Este livro já está emprestado e ainda não foi devolvido.", exception.Message);

            emprestimoRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()), Times.Never);
            livroRepositoryMock.Verify(x => x.MarcarComoIndisponivel(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Executar_QuandoLivroNaoEstaEmprestado_DeveCadastrarEmprestimoEMarcarLivroComoIndisponivel()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            emprestimoRepositoryMock
                .Setup(x => x.LivroEstaEmprestadoAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            emprestimoRepositoryMock
                .Setup(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()))
                .ReturnsAsync(1);

            CadastrarEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            CadastrarEmprestimoInputDTO input = new()
            {
                IdUsuario = 1,
                IdLivro = 1,
                DataPrevistaDevolucao = DateTime.Now.AddDays(3)
            };

            int idEmprestimo = await useCase.Executar(input);

            Assert.Equal(1, idEmprestimo);

            emprestimoRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()), Times.Once);
            livroRepositoryMock.Verify(x => x.MarcarComoIndisponivel(input.IdLivro), Times.Once);
        }

        [Fact]
        public async Task Executar_QuandoUsuarioPossuiEmprestimoEmAtraso_DeveLancarExcecao()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            emprestimoRepositoryMock
                .Setup(x => x.UsuarioPossuiEmprestimoEmAtrasoAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            CadastrarEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            CadastrarEmprestimoInputDTO input = new()
            {
                IdUsuario = 1,
                IdLivro = 1,
                DataPrevistaDevolucao = DateTime.Now.AddDays(3)
            };

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Executar(input));

            Assert.Equal("Usuário com empréstimo em atraso não pode realizar novo empréstimo.", exception.Message);

            emprestimoRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()), Times.Never);
            livroRepositoryMock.Verify(x => x.MarcarComoIndisponivel(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Executar_QuandoUsuarioPossuiEmprestimoEmAtraso_DeveLancarExcecaoEAtualizarFlagDeAtraso()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            emprestimoRepositoryMock
                .Setup(x => x.UsuarioPossuiEmprestimoEmAtrasoAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            CadastrarEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            CadastrarEmprestimoInputDTO input = new()
            {
                IdUsuario = 1,
                IdLivro = 1,
                DataPrevistaDevolucao = DateTime.Now.AddDays(3)
            };

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Executar(input));

            Assert.Equal("Usuário com empréstimo em atraso não pode realizar novo empréstimo.", exception.Message);

            usuarioRepositoryMock.Verify(x => x.AtualizarPossuiAtrasoAtivoAsync(input.IdUsuario, true), Times.Once);
            emprestimoRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()), Times.Never);
            livroRepositoryMock.Verify(x => x.MarcarComoIndisponivel(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Executar_QuandoUsuarioNaoPossuiEmprestimoEmAtraso_DeveAtualizarFlagParaFalsoECadastrarEmprestimo()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            emprestimoRepositoryMock
                .Setup(x => x.UsuarioPossuiEmprestimoEmAtrasoAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            emprestimoRepositoryMock
                .Setup(x => x.LivroEstaEmprestadoAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            emprestimoRepositoryMock
                .Setup(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()))
                .ReturnsAsync(1);

            CadastrarEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            CadastrarEmprestimoInputDTO input = new()
            {
                IdUsuario = 1,
                IdLivro = 1,
                DataPrevistaDevolucao = DateTime.Now.AddDays(3)
            };

            int idEmprestimo = await useCase.Executar(input);

            Assert.Equal(1, idEmprestimo);

            usuarioRepositoryMock.Verify(x => x.AtualizarPossuiAtrasoAtivoAsync(input.IdUsuario, false), Times.Once);
            emprestimoRepositoryMock.Verify(x => x.Cadastrar(It.IsAny<EmprestimoEntity>()), Times.Once);
            livroRepositoryMock.Verify(x => x.MarcarComoIndisponivel(input.IdLivro), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveAtualizarFlagDeAtrasoDoUsuarioAposDevolucao()
        {
            Mock<IEmprestimoRepository> emprestimoRepositoryMock = new();
            Mock<ILivroRepository> livroRepositoryMock = new();
            Mock<IUsuarioRepository> usuarioRepositoryMock = new();

            EmprestimoEntity emprestimo = new();

            emprestimo.Cadastrar(1, 1, DateTime.Now.AddDays(7));
            emprestimo.Id = 1;

            emprestimoRepositoryMock
                .Setup(x => x.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(emprestimo);

            emprestimoRepositoryMock
                .Setup(x => x.UsuarioPossuiEmprestimoEmAtrasoAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            DevolverEmprestimoUC useCase = new(emprestimoRepositoryMock.Object, livroRepositoryMock.Object, usuarioRepositoryMock.Object);

            DevolverEmprestimoInputDTO input = new()
            {
                IdEmprestimo = 1
            };

            int idEmprestimo = await useCase.Executar(input);

            Assert.Equal(emprestimo.Id, idEmprestimo);

            usuarioRepositoryMock.Verify(x => x.AtualizarPossuiAtrasoAtivoAsync(emprestimo.IdUsuario, false), Times.Once);
            emprestimoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<EmprestimoEntity>()), Times.Once);
            livroRepositoryMock.Verify(x => x.MarcarComoDisponivel(emprestimo.IdLivro), Times.Once);
        }
    }
}
