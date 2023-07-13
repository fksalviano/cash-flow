using Domain.Abstractions;
using Application.UseCases.GetDailyBalance.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetDailyBalance;

public class GetDailyBalanceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services            
            .AddScoped<IGetDailyBalanceUseCase, GetDailyBalanceUseCase>();
}
