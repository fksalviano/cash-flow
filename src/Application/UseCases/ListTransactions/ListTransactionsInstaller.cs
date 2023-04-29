using Application.Commons.Abstractions;
using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.ListTransactions;

public class ListTransactionsInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services
            .AddScoped<IListTransactionsRepository, ListTransactionsRepository>()
            .AddScoped<IListTransactionsUseCase, ListTransactionsUseCase>();
}
