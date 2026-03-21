using BibliotecaApi.UseCases.Emprestimo.DTO;
using FluentValidation;

namespace BibliotecaApi.Application.Api.Validators.Emprestimo
{
    public class CadastrarEmprestimoInputDTOValidator : AbstractValidator<CadastrarEmprestimoInputDTO>
    {
        public CadastrarEmprestimoInputDTOValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0)
                .WithMessage("IdUsuario deve ser maior que zero.");

            RuleFor(x => x.IdLivro)
                .GreaterThan(0)
                .WithMessage("IdLivro deve ser maior que zero.");

            RuleFor(x => x.DataPrevistaDevolucao)
                .NotEmpty()
                .WithMessage("Data prevista de devolução é obrigatória.");
        }
    }
}