using FluentValidation;
using Intellimen.Communication.Requests;
using Intellimen.Exceptions;

namespace Intellimen.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequestRegistrarUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage(RespostaMensagensException.NOME_VAZIO);
        RuleFor(u => u.Email).NotEmpty().WithMessage(RespostaMensagensException.EMAIL_VAZIO) ;
        RuleFor(u => u.Email).EmailAddress().WithMessage(RespostaMensagensException.EMAIL_VALIDO); 
        RuleFor(u => u.Senha.Length).NotEmpty().WithMessage(RespostaMensagensException.SENHA_VAZIA);
        RuleFor(u => u.Senha.Length).GreaterThan(6).LessThanOrEqualTo(50).WithMessage(RespostaMensagensException.SENHA_VALIDA);
    }
}
