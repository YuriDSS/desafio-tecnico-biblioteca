using BibliotecaApi.Domain.Shared;
using BibliotecaApi.UseCases.Usuario.DTO;
using FluentValidation;

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
                .Must(TerOnzeDigitosNumericos)
                .WithMessage("CPF deve conter 11 dígitos numéricos.");
        }

        private static bool TerOnzeDigitosNumericos(string cpf)
        {
            string cpfNormalizado = CpfHelper.Normalizar(cpf);

            bool cpfPossuiOnzeDigitos = cpfNormalizado.Length == 11;

            return cpfPossuiOnzeDigitos;
        }
    }
}