using Application.UseCases.GetDailyBalance.Domain;
using Application.UseCases.GetDailyBalance.Ports;

namespace Application.UseCases.GetDailyBalance.Extensions;

public static class GetDailyBalanceExtensions
{
    public static GetDailyBalanceOutput ToOutput(this IEnumerable<DailyBalance> balances) =>
        new(balances);
}
