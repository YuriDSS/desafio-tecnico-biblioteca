using BibliotecaApi.UseCases.Emprestimo.DTO;
using FluentValidation;

namespace BibliotecaApi.Application.Api.Validators.Emprestimo
{
    public class DevolverEmprestimoInputDTOValidator : AbstractValidator<DevolverEmprestimoInputDTO>
    {
        public DevolverEmprestimoInputDTOValidator()
        {
            RuleFor(x => x.IdEmprestimo)
                .GreaterThan(0)
                .WithMessage("IdEmprestimo deve ser maior que zero.");

            RuleFor(x => x.DataDevolucao)
                .NotEmpty()
                .WithMessage("Data de devolução é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data de devolução não pode ser futura.");
        }
    }
}