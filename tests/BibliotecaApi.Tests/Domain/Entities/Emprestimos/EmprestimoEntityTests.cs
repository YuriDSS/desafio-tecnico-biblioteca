using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Tests.Domain.Entities.Emprestimos
{
    public class EmprestimoEntityTests
    {
        [Fact]
        public void RegistrarDevolucao_QuandoDevolvidoNoPrazo_DeveGerarMultaZero()
        {
            EmprestimoEntity emprestimo = new();
            DateTime dataPrevista = DateTime.Now.AddDays(1);

            emprestimo.Cadastrar(idUsuario: 1, idLivro: 1, dataPrevista: dataPrevista);

            DateTime dataDevolucao = dataPrevista;

            emprestimo.RegistrarDevolucao(dataDevolucao);

            Assert.Equal(0m, emprestimo.Multa);
            Assert.Equal(5.00m, emprestimo.Total);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);
        }

        [Fact]
        public void RegistrarDevolucao_QuandoDevolvidoAntesDoPrazo_DeveGerarMultaZero()
        {
            EmprestimoEntity emprestimo = new();
            DateTime dataPrevista = DateTime.Now.AddDays(3);

            emprestimo.Cadastrar(idUsuario: 1, idLivro: 1, dataPrevista: dataPrevista);

            DateTime dataDevolucao = dataPrevista.AddDays(-1);

            emprestimo.RegistrarDevolucao(dataDevolucao);

            Assert.Equal(0m, emprestimo.Multa);
            Assert.Equal(5.00m, emprestimo.Total);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);
        }

        [Fact]
        public void RegistrarDevolucao_QuandoDevolvidoComDoisDiasDeAtraso_DeveGerarMultaCorreta()
        {
            EmprestimoEntity emprestimo = new();
            DateTime dataPrevista = DateTime.Now.AddDays(3);

            emprestimo.Cadastrar(idUsuario: 1, idLivro: 1, dataPrevista: dataPrevista);

            DateTime dataDevolucao = dataPrevista.AddDays(2);

            emprestimo.RegistrarDevolucao(dataDevolucao);

            Assert.Equal(4.00m, emprestimo.Multa);
            Assert.Equal(9.00m, emprestimo.Total);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);
        }

        [Fact]
        public void RegistrarDevolucao_QuandoDevolvidoComCincoDiasDeAtraso_DeveGerarMultaCorreta()
        {
            EmprestimoEntity emprestimo = new();
            DateTime dataPrevista = DateTime.Now.AddDays(3);

            emprestimo.Cadastrar(idUsuario: 1, idLivro: 1, dataPrevista: dataPrevista);

            DateTime dataDevolucao = dataPrevista.AddDays(5);

            emprestimo.RegistrarDevolucao(dataDevolucao);

            Assert.Equal(13.00m, emprestimo.Multa);
            Assert.Equal(18.00m, emprestimo.Total);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);
        }

        [Fact]
        public void RegistrarDevolucao_QuandoDevolvidoComVinteDiasDeAtraso_DeveLimitarMultaEmCinquentaReais()
        {
            EmprestimoEntity emprestimo = new();
            DateTime dataPrevista = DateTime.Now.AddDays(3);

            emprestimo.Cadastrar(idUsuario: 1, idLivro: 1, dataPrevista: dataPrevista);

            DateTime dataDevolucao = dataPrevista.AddDays(20);

            emprestimo.RegistrarDevolucao(dataDevolucao);

            Assert.Equal(50.00m, emprestimo.Multa);
            Assert.Equal(55.00m, emprestimo.Total);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);
        }
    }
}