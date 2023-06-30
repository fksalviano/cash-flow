using Microsoft.Extensions.DependencyInjection;

namespace Domain.Abstractions;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services);
}