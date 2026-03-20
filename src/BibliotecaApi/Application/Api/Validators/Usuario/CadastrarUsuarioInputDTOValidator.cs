using BibliotecaApi.UseCases.Usuario.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BibliotecaApi.Application.Api.Validators.Usuario
{
    public class CadastrarUsuarioInputDTOValidator : AbstractValidator<CadastrarUsuarioInputDTO>
    {
        public CadastrarUsuarioInputDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório.")
                .EmailAddress()
                .WithMessage("Email inválido.");

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório.")
                .Must(TerFormatoValido)
                .WithMessage("CPF deve conter 11 dígitos numéricos.");
        }

        private static bool TerFormatoValido(string cpf)
        {
            bool cpfInvalido = string.IsNullOrWhiteSpace(cpf);
            if (cpfInvalido)
            {
                return false;
            }

            bool cpfValido = Regex.IsMatch(cpf, @"^\d{11}$") || Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
            return cpfValido;
        }
    }
}