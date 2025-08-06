using FluentValidation;
using Intellimen.Business.MessagesExceptions;
using Intellimen.Business.Requests;

namespace Intellimen.Business.Services.Application.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(DefaultExceptions.EMAIL_EMPTY)
                .EmailAddress().WithMessage(DefaultExceptions.INVALID_EMAIL);
        }
    }
}
