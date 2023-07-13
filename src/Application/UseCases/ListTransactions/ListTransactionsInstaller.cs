using Domain.Abstractions;
using Application.UseCases.ListTransactions.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.ListTransactions;

public class ListTransactionsInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services            
            .AddScoped<IListTransactionsUseCase, ListTransactionsUseCase>();
}
