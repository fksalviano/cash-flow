using Application.UseCases.GetDailyBalance.Domain;

namespace Application.UseCases.GetDailyBalance.Ports;

public record GetDailyBalanceOutput
(
    IEnumerable<DailyBalance> DailyBalances
);
