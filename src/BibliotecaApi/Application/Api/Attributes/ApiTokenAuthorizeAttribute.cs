using BibliotecaApi.Application.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Application.Api.Attributes
{
    public class ApiTokenAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiTokenAuthorizeAttribute() : base(typeof(ApiTokenAuthorizationFilter))
        {
        }
    }
}