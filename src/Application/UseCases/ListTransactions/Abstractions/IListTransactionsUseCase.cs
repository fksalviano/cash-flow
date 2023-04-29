namespace Application.UseCases.ListTransactions.Abstractions;

public interface IListTransactionsUseCase
{
    Task ExecuteAsync(CancellationToken cancellationToken);
    void SetOutputPort(IListTransactionsOutputPort outputPort);
}
