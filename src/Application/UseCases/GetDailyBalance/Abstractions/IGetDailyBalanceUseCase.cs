using Application.UseCases.GetDailyBalance.Abstractions;

namespace Application.UseCases.GetDailyBalance.Abstractions;

public interface IGetDailyBalanceUseCase
{
    Task ExecuteAsync(CancellationToken cancellationToken);
    void SetOutputPort(IGetDailyBalanceOutputPort outputPort);
}
