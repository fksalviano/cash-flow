using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Extensions;
using Application.UseCases.SaveTransaction.Ports;

namespace Application.UseCases.SaveTransaction;

public class SaveTransactionUseCase : ISaveTransactionUseCase
{
    private readonly ISaveTransactionRepository _repository;
    private ISaveTransactionOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveTransactionOutputPort outputPort) =>
        _outputPort = outputPort;

    public SaveTransactionUseCase(ISaveTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SaveTransactionInput input, CancellationToken cancellationToken)
    {
        var transaction = input.ToTransaction();

        var transactionSaved = await _repository.SaveAsync(transaction, cancellationToken);
        if (!transactionSaved)
        {
            _outputPort.Error("Error to Save Transaction");
            return;
        }

        var output = transaction.ToOutput();
        _outputPort.Ok(output);
    }
}
