using BibliotecaApi.UseCases.Livro.DTO;

namespace BibliotecaApi.Tests.Fixtures
{
    public class LivroFixture
    {
        public string TituloValido => "Clean Code";
        public string AutorValido => "Robert C. Martin";
        public string IsbnValido => "9788533302273";

        public CadastrarLivroInputDTO CriarInputValido()
        {
            return new CadastrarLivroInputDTO
            {
                Titulo = TituloValido,
                Autor = AutorValido,
                ISBN = IsbnValido
            };
        }

        public CadastrarLivroInputDTO CriarInputComIsbn(string isbn)
        {
            return new CadastrarLivroInputDTO
            {
                Titulo = TituloValido,
                Autor = AutorValido,
                ISBN = isbn
            };
        }
    }
}