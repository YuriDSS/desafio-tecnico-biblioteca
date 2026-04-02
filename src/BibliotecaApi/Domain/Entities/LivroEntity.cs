namespace BibliotecaApi.Domain.Entities;

public sealed class LivroEntity
{
    public int? Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public string ISBN { get; private set; } = string.Empty;

    public LivroEntity()
    {

    }
    public LivroEntity(int? id, string titulo, string autor, string isbn)
    {
        if (id.HasValue)
        {
            if (id.Value <= 0)
            {
                throw new ArgumentException("Id deve ser maior que zero.");
            }

            Id = id.Value;
        }

        ValidarDados(titulo, autor, isbn);

        Titulo = titulo;
        Autor = autor;
        ISBN = isbn;
    }

    public void Cadastrar(string titulo, string autor, string isbn)
    {

        ValidarDados(titulo, autor, isbn);

        Titulo = titulo;
        Autor = autor;
        ISBN = isbn;
    }

    private void ValidarDados(string titulo, string autor, string isbn)
    {
        bool tituloVazio = string.IsNullOrWhiteSpace(titulo);
        if (tituloVazio)
        {
            throw new ArgumentException("Título não pode ser vazio.");
        }

        bool autorVazio = string.IsNullOrWhiteSpace(autor);
        if (autorVazio)
        {
            throw new ArgumentException("Autor não pode ser vazio.");
        }

        bool isbnVazio = string.IsNullOrWhiteSpace(isbn);
        if (isbnVazio)
        {
            throw new ArgumentException("ISBN não pode ser vazio.");
        }

        bool isbnNaoPossuiTrezeDigitos = isbn.Length != 13 || isbn.Any(c => !char.IsDigit(c));
        if (isbnNaoPossuiTrezeDigitos)
        {
            throw new ArgumentException("ISBN deve conter exatamente 13 dígitos numéricos.");
        }
    }
}