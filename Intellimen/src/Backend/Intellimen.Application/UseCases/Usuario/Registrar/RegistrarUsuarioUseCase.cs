using Intellimen.Communication.Requests;
using Intellimen.Communication.Responses;

namespace Intellimen.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase
{
    public ResponseRegistrarUsuarioJson Execute(RequestRegistrarUsuarioJson request)
    {
        // Validar a request
        Validate(request);

        // Mapear a request em uma entidade

        // Criptografia de senha

        // Salvar no Banco de dados

        return new ResponseRegistrarUsuarioJson
        {
            Nome = request.Nome
        };
    }

    private void Validate(RequestRegistrarUsuarioJson request)
    {
        var validar = new RegistrarUsuarioValidator();
        var result = validar.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage);
            throw new Exception();
        }
    }
}
