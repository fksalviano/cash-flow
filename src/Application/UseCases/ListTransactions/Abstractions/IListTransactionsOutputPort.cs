using Application.UseCases.ListTransactions.Ports;

namespace Application.UseCases.ListTransactions.Abstractions;

public interface IListTransactionsOutputPort
{
    void Ok(ListTransactionsOutput output);
    void NotFound();
    void Error(string errorMessage);
}