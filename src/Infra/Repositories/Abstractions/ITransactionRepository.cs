using Domain.Models;

namespace Infra.Repositories.Abstractions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>?> GetTransactionsAsync(CancellationToken cancellationToken);        
        Task<bool> SaveTransactionAsync(Transaction transaction, CancellationToken cancellationToken);

        Task<IEnumerable<DailyBalance>?> GetDailyBalancesAsync(CancellationToken cancellationToken);
    }
}