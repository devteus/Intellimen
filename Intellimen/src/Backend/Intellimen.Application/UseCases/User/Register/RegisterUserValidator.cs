using FluentValidation;
using Intellimen.Communication.Requests;
using Intellimen.Exceptions;

namespace Intellimen.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage(ResponseMessagesException.NOME_VAZIO);
        RuleFor(u => u.Email).NotEmpty().WithMessage(ResponseMessagesException.EMAIL_VAZIO) ;
        RuleFor(u => u.Email).EmailAddress().WithMessage(ResponseMessagesException.EMAIL_VALIDO); 
        RuleFor(u => u.Senha.Length).NotEmpty().WithMessage(ResponseMessagesException.SENHA_VAZIA);
        RuleFor(u => u.Senha.Length).GreaterThan(6).LessThanOrEqualTo(50).WithMessage(ResponseMessagesException.SENHA_VALIDA);
    }
}
