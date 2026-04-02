namespace BibliotecaApi.UseCases.Emprestimo.DTO;

public class DevolverEmprestimoOutputDTO
{
    public int Id { get; set; }
    public DateTime DataDevolucao { get; set; }
    public decimal Multa { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}