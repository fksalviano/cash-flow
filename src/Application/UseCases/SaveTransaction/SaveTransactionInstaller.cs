using System.Diagnostics.CodeAnalysis;
using Domain.Abstractions;
using Application.Commons.Extensions;
using Application.UseCases.SaveTransaction.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.SaveTransaction;

[ExcludeFromCodeCoverage]
public class SaveTransactionInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services) =>
        services
            .AddScopedWithValidation<ISaveTransactionUseCase, SaveTransactionUseCase>(useCase =>
                new SaveTransactionUseCaseValidation(useCase));
}
