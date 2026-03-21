namespace BibliotecaApi.UseCases.Livro.DTO
{
    public class ListarLivrosOutputDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
    }
}