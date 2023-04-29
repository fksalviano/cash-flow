using Application.UseCases.GetDailyBalance.Ports;

namespace Application.UseCases.GetDailyBalance.Abstractions;

public interface IGetDailyBalanceOutputPort
{
    void Ok(GetDailyBalanceOutput output);
    void NotFound();
    void Error(string errorMessage);
}
