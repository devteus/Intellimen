using Intellimen.Communication.Requests;
using Intellimen.Communication.Responses;

namespace Intellimen.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
    {
        // Validar a request
        Validate(request);

        // Mapear a request em uma entidade

        // Criptografia de senha

        // Salvar no Banco de dados

        return new ResponseRegisterUserJson
        {
            Nome = request.Nome
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var valid = new RegisterUserValidator();
        var result = valid.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage);
            throw new Exception();
        }
    }
}
