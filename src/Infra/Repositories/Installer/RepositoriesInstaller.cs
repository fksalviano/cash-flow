using Domain.Abstractions;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repositories.Installer
{
    public class RepositoriesInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services
                .AddSingleton<ITransactionRepository, TransactionRepository>();
        }
    }
}