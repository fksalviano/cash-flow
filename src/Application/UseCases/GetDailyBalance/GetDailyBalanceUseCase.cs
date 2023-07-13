using Application.UseCases.GetDailyBalance.Abstractions;
using Domain.Models;
using Application.UseCases.GetDailyBalance.Extensions;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.GetDailyBalance;

public class GetDailyBalanceUseCase : IGetDailyBalanceUseCase
{
    private readonly ITransactionRepository _repository;
    private IGetDailyBalanceOutputPort _outputPort = null!;

    public void SetOutputPort(IGetDailyBalanceOutputPort outputPort) =>
        _outputPort = outputPort;

    public GetDailyBalanceUseCase(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var balances = await _repository.GetDailyBalancesAsync(cancellationToken);

        if (balances == null)
        {
            _outputPort.Error("Error to Get Balances");
            return;
        }

        if (!balances.Any())
        {
            _outputPort.NotFound();
            return;
        }

        decimal initialBalance = 0;
        balances.UpdateCurrentBalances(initialBalance);

        var output = balances.ToOutput();
        _outputPort.Ok(output);
    }
}
