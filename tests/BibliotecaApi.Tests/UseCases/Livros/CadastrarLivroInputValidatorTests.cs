using BibliotecaApi.Tests.Fixtures;
using BibliotecaApi.Application.Api.Validators.Livro;
using FluentValidation.Results;
using BibliotecaApi.UseCases.Livro.DTO;

namespace BibliotecaApi.Tests.UseCases.Livro.Validators
{
    public class CadastrarLivroInputValidatorTests
    {
        private readonly LivroFixture _fixture = new();

        [Fact]
        public void Deve_Passar_Quando_Isbn_For_Valido()
        {
            CadastrarLivroInputDTOValidator validator = new();
            CadastrarLivroInputDTO input = _fixture.CriarInputValido();

            ValidationResult resultado = validator.Validate(input);

            Assert.True(resultado.IsValid);
        }

        [Fact]
        public void Deve_Falhar_Quando_Isbn_For_Invalido()
        {
            CadastrarLivroInputDTOValidator validator = new();
            CadastrarLivroInputDTO input = _fixture.CriarInputComIsbn("123");

            ValidationResult resultado = validator.Validate(input);

            Assert.False(resultado.IsValid);
            Assert.Contains(resultado.Errors, e => e.PropertyName == "ISBN" && e.ErrorMessage.Contains("13 dígitos")); 
        }
    }
}