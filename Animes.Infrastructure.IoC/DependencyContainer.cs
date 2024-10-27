using Animes.Application.Interfaces;
using Animes.Application.Services;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Animes.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Animes.Application
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            //Animes.Domain.Interfaces e Animes.Infra.Data.Repositories
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<IDiretorRepository, DiretorRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}