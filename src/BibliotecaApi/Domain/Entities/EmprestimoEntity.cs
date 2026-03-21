namespace BibliotecaApi.Domain.Entities;

public class EmprestimoEntity
{
    public int Id { get; set; }
    public int IdUsuario { get; private set; }
    public int IdLivro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public DateTime? DataDevolucao { get; private set; }
    public Decimal Valor { get; private set; }
    public Decimal Multa { get; private set; }
    public Decimal Total { get; private set; }

    public void Cadastrar(int idUsuario, int idLivro, DateTime dataPrevista)
    {
        bool usuarioInvalido = idUsuario <= 0;
        if (usuarioInvalido)
        {
            throw new Exception("Usuário inválido.");
        }

        bool livroInvalido = idLivro <= 0;
        if (livroInvalido)
        {
            throw new Exception("Livro inválido.");
        }

        bool dataDevolucaoInvalida = dataPrevista <= DateTime.Now;
        if (dataDevolucaoInvalida)
        {
            throw new Exception("A data prevista de devolução deve ser futura.");
        }

        IdUsuario = idUsuario;
        IdLivro = idLivro;
        DataEmprestimo = DateTime.Now;
        DataPrevistaDevolucao = dataPrevista;
        DataDevolucao = null;
        Valor = 5.00m;
        Multa = 0m;
        Total = 0m;
    }

    public void RegistrarDevolucao(DateTime? dataDevolucao = null)
    {
        DataDevolucao = dataDevolucao ?? DateTime.Now;
        Multa = CalcularMulta();
        Total = Valor + Multa;
    }

    private decimal CalcularMulta()
    {
        bool dataDevolucaoInvalida = DataDevolucao == null;
        if (dataDevolucaoInvalida)
        {
            throw new Exception("Empréstimo ainda não devolvido.");
        }

        bool devolucaoAntesDoPrazo = DataDevolucao.Value <= DataPrevistaDevolucao;
        if (devolucaoAntesDoPrazo)
        {
            return 0m;
        }

        int diasAtraso = (DataDevolucao.Value.Date - DataPrevistaDevolucao.Date).Days;

        bool devolucaoSemAtraso = diasAtraso <= 0;
        if (devolucaoSemAtraso)
        {
            return 0m;
        }

        return diasAtraso * 2.00m;
    }
}
