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

            if (string.IsNullOrWhiteSpace(authorizationHeader) || string.IsNullOrWhiteSpace(tokenConfigurado))
            {
                RetornarNaoAutorizado(context);
                return;
            }

            const string bearerPrefix = "Bearer ";

            if (!authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                RetornarNaoAutorizado(context);
                return;
            }

            string tokenRecebido = authorizationHeader[bearerPrefix.Length..].Trim();

            if (!string.Equals(tokenRecebido, tokenConfigurado, StringComparison.Ordinal))
            {
                RetornarNaoAutorizado(context);
            }
        }

        private static void RetornarNaoAutorizado(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                sucesso = false,
                conteudo = (object?)null,
                mensagem_erro = "Token de acesso inválido."
            });
        }
    }
}