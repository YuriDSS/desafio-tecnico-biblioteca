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
        bool emprestimoJaFoiDevolvido = DataDevolucao.HasValue;
        if (emprestimoJaFoiDevolvido)
        {
            throw new Exception("Este empréstimo já foi devolvido.");
        }

        DateTime dataEfetivaDevolucao = dataDevolucao ?? DateTime.Now;

        if (dataEfetivaDevolucao < DataEmprestimo)
        {
            throw new Exception("A data de devolução não pode ser anterior à data do empréstimo.");
        }

        bool bloquearDevolucaoDataFutura = dataEfetivaDevolucao > DateTime.Now;
        if (bloquearDevolucaoDataFutura)
        {
            throw new Exception("A data de devolução não pode ser futura.");
        }

        DataDevolucao = dataEfetivaDevolucao;
        Multa = CalcularMulta();
        Total = Valor + Multa;
    }

    private decimal CalcularMulta()
    {
        if (DataDevolucao is null)
        {
            throw new Exception("Empréstimo ainda não devolvido.");
        }

        DateTime dataDevolucao = DataDevolucao.Value;

        bool devolucaoNoPrazoOuAntes = dataDevolucao <= DataPrevistaDevolucao;
        if (devolucaoNoPrazoOuAntes)
        {
            return 0m;
        }

        int diasAtraso = (dataDevolucao.Date - DataPrevistaDevolucao.Date).Days;

        bool devolucaoSemAtraso = diasAtraso <= 0;
        if (devolucaoSemAtraso)
        {
            return 0m;
        }

        decimal multa = 0m;

        int diasPrimeiraFaixa = Math.Min(diasAtraso, 3);
        multa += diasPrimeiraFaixa * 2.00m;

        if (diasAtraso > 3)
        {
            int diasSegundaFaixa = diasAtraso - 3;
            multa += diasSegundaFaixa * 3.50m;
        }

        decimal limiteMaximo = 50.00m;

        if (multa > limiteMaximo)
        {
            multa = limiteMaximo;
        }

        return multa;
    }
}
