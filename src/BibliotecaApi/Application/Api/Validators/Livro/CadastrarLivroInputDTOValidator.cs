using BibliotecaApi.UseCases.Livro.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BibliotecaApi.Application.Api.Validators.Livro
{
    public class CadastrarLivroInputDTOValidator : AbstractValidator<CadastrarLivroInputDTO>
    {
        public CadastrarLivroInputDTOValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Título é obrigatório.");

            RuleFor(x => x.Autor)
                .NotEmpty()
                .WithMessage("Autor é obrigatório.");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .WithMessage("ISBN é obrigatório.")
                .Must(TerTrezeDigitosNumericos)
                .WithMessage("ISBN deve conter exatamente 13 dígitos numéricos.");
        }

        private static bool TerTrezeDigitosNumericos(string isbn)
        {
            string isbnNormalizado = Regex.Replace(isbn ?? string.Empty, @"\D", string.Empty);

            bool isbnPossuiTrezeDigitos = isbnNormalizado.Length == 13;

            return isbnPossuiTrezeDigitos;
        }
    }
}