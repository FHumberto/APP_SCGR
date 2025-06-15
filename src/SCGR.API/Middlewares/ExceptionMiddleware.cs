using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace SCGR.API.Middlewares;

/// <summary>
/// Middleware responsável pelo tratamento global de exceções não tratadas na aplicação.
/// Implementa a interface <see cref="IExceptionHandler"/> para interceptar erros e
/// retornar um <see cref="ProblemDetails"/> apropriado ao cliente.
/// </summary>
public sealed class ExceptionMiddleware : IExceptionHandler
{
    /// <summary>
    /// Intercepta a exceção lançada, configura o <see cref="ProblemDetails"/> conforme o tipo de erro
    /// e escreve a resposta JSON no <see cref="HttpContext.Response"/>.
    /// </summary>
    /// <param name="httpContext">Contexto HTTP da requisição.</param>
    /// <param name="exception">Exceção capturada a ser tratada.</param>
    /// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
    /// <returns>
    /// <c>true</c> se a exceção foi tratada e a resposta escrita; caso contrário, <c>false</c>.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem = new();

        switch (exception)
        {
            case InvalidOperationException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1";
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Operação inválida.";
                break;

            case UnauthorizedAccessException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.2";
                problem.Status = StatusCodes.Status403Forbidden;
                problem.Title = "Acesso negado ao recurso solicitado.";
                break;

            case IOException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.4";
                problem.Status = StatusCodes.Status503ServiceUnavailable;
                problem.Title = "Erro de entrada/saída ao processar a solicitação.";
                break;

            case NotImplementedException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.2";
                problem.Status = StatusCodes.Status501NotImplemented;
                problem.Title = "Este recurso ainda não está disponível.";
                break;

            default:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1";
                problem.Title = "Erro Interno do Servidor. Por favor, tente novamente mais tarde.";
                problem.Status = StatusCodes.Status500InternalServerError;
                break;
        }

        httpContext.Response.StatusCode = problem.Status.Value;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}