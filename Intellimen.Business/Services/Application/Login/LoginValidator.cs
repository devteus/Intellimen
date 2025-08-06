using FluentValidation;
using Intellimen.Business.Requests;

namespace Intellimen.Business.Services.Application.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("Formato de email inválido");
        }
    }
}
