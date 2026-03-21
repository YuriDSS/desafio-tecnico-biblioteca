using BibliotecaApi.Domain.Entities;

namespace BibliotecaApi.Tests.Domain.Entities
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
        public void RegistrarDevolucao_QuandoDevolvidoComAtraso_DeveGerarMultaCorreta()
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
    }
}