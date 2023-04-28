using Application.UseCases.SaveTransaction.Ports;

namespace Application.UseCases.SaveTransaction.Abstractions;

public interface ISaveTransactionUseCase
{
    Task ExecuteAsync(SaveTransactionInput input, CancellationToken cancellationToken);

    void SetOutputPort(ISaveTransactionOutputPort outputPort);
}
