using Application.Domain;

namespace Application.UseCases.ListTransactions.Ports;

public record ListTransactionsOutput
(
    IEnumerable<Transaction> Transactions
);
