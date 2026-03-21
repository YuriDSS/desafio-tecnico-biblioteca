namespace BibliotecaApi.UseCases.Livro.DTO;

public class CadastrarLivroInputDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;

}
