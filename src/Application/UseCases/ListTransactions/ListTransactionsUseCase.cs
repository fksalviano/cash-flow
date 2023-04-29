using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Extensions;

namespace Application.UseCases.ListTransactions;

public class ListTransactionsUseCase : IListTransactionsUseCase
{
    private readonly IListTransactionsRepository _repository;    
    private IListTransactionsOutputPort _outputPort = null!;    

    public void SetOutputPort(IListTransactionsOutputPort outputPort) =>
        _outputPort = outputPort;

    public ListTransactionsUseCase(IListTransactionsRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetTransactionsAsync(cancellationToken);

        if (transactions == null)
        {
            _outputPort.Error("Error to Get Transactions");
            return;
        }

        if (!transactions.Any())
        {
            _outputPort.NotFound();
            return;
        }

        var output = transactions.ToOutput();
        _outputPort.Ok(output);
    }    
}
