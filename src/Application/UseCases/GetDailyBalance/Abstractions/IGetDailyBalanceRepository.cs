using Application.UseCases.GetDailyBalance.Domain;

namespace Application.UseCases.GetDailyBalance.Abstractions;

public interface IGetDailyBalanceRepository
{
    Task<IEnumerable<DailyBalance>?> GetDailyBalancesAsync(CancellationToken cancellationToken);
}
