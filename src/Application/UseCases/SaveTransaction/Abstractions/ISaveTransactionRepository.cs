using Application.Domain;

namespace Application.UseCases.SaveTransaction.Abstractions;

public interface ISaveTransactionRepository
{
    Task<bool> SaveAsync(Transaction transaction, CancellationToken cancellationToken);
}
