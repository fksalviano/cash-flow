using System.Diagnostics.CodeAnalysis;
using Application.Commons.Abstractions;
using Application.Commons.Extensions;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.SaveTransaction;

[ExcludeFromCodeCoverage]
public class SaveTransactionInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services
            .AddScoped<ISaveTransactionRepository, SaveTransactionRepository>()
            .AddScopedWithValidation<ISaveTransactionUseCase, SaveTransactionUseCase>(useCase =>
                new SaveTransactionUseCaseValidation(useCase));
}
