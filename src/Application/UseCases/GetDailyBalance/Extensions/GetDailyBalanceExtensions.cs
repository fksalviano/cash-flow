using Application.UseCases.GetDailyBalance.Domain;
using Application.UseCases.GetDailyBalance.Ports;

namespace Application.UseCases.GetDailyBalance.Extensions;

public static class GetDailyBalanceExtensions
{
    public static GetDailyBalanceOutput ToOutput(this IEnumerable<DailyBalance> balances) =>
        new(balances);

    public static void UpdateCurrentBalances(this IEnumerable<DailyBalance> balances, decimal initialBalance)
    {
        decimal currentBalance = initialBalance;
        
        foreach(var balance in balances)
        {
            currentBalance += balance.DayBalance;
            balance.SetCurrentBalance(currentBalance);
        }
    }
}
