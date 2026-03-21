using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BibliotecaApi.Application.Api.Filters
{
    public class ApiTokenAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiTokenAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? authorizationHeader = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
            string? tokenConfigurado = _configuration["ApiTokenSettings:Token"];

            bool headerAusente = string.IsNullOrWhiteSpace(authorizationHeader);
            bool tokenConfiguradoAusente = string.IsNullOrWhiteSpace(tokenConfigurado);

            if (headerAusente || tokenConfiguradoAusente)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    sucesso = false,
                    conteudo = (object?)null,
                    mensagem_erro = "Token de acesso inválido."
                });

                return;
            }

            const string bearerPrefix = "Bearer ";

            bool formatoInvalido = !authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase);
            if (formatoInvalido)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    sucesso = false,
                    conteudo = (object?)null,
                    mensagem_erro = "Token de acesso inválido."
                });

                return;
            }

            string tokenRecebido = authorizationHeader[bearerPrefix.Length..].Trim();

            bool tokenInvalido = !string.Equals(tokenRecebido, tokenConfigurado, StringComparison.Ordinal);
            if (tokenInvalido)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    sucesso = false,
                    conteudo = (object?)null,
                    mensagem_erro = "Token de acesso inválido."
                });

                return;
            }
        }
    }
}