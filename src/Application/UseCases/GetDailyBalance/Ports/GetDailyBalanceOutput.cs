using Domain.Models;

namespace Application.UseCases.GetDailyBalance.Ports;

public record GetDailyBalanceOutput
(
    IEnumerable<DailyBalance> DailyBalances
);
