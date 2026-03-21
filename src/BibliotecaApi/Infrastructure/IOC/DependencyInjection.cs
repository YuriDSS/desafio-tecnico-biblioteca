using BibliotecaApi.Infrastructure.Data;
using BibliotecaApi.Infrastructure.Repositories;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Emprestimo;
using BibliotecaApi.UseCases.Livro;
using BibliotecaApi.UseCases.Usuario;
using BibliotecaApi.Application.Api.Filters;

namespace BibliotecaApi.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfraestrutura()
                .AddRepositorios()
                .AddCasosDeUso()
                .AddFiltros();

            return services;
        }

        public static IServiceCollection AddInfraestrutura(this IServiceCollection services)
        {
            services.AddScoped<DbSession>();

            return services;
        }

        private static IServiceCollection AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

            return services;
        }

        private static IServiceCollection AddCasosDeUso(this IServiceCollection services)
        {
            services.AddScoped<CadastrarUsuarioUC>();
            services.AddScoped<CadastrarLivroUC>();
            services.AddScoped<ListarLivrosUC>();
            services.AddScoped<CadastrarEmprestimoUC>();
            services.AddScoped<DevolverEmprestimoUC>();

            return services;
        }

        private static IServiceCollection AddFiltros(this IServiceCollection services)
        {
            services.AddScoped<ApiTokenAuthorizationFilter>();

            return services;
        }
    }
}