using Application.Domain;

namespace Application.UseCases.ListTransactions.Abstractions;

public interface IListTransactionsRepository
{
    Task<IEnumerable<Transaction>?> GetTransactionsAsync(CancellationToken cancellationToken);
}
