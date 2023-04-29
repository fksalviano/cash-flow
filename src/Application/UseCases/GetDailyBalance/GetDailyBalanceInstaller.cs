using Application.Commons.Abstractions;
using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetDailyBalance;

public class GetDailyBalanceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services
            .AddScoped<IGetDailyBalanceRepository, GetDailyBalanceRepository>()
            .AddScoped<IGetDailyBalanceUseCase, GetDailyBalanceUseCase>();
}
