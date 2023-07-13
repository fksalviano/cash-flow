using Domain.Models;
using Application.UseCases.ListTransactions.Ports;

namespace Application.UseCases.ListTransactions.Extensions;

public static class ListTransactionsExtensions
{
    public static ListTransactionsOutput ToOutput(this IEnumerable<Transaction> transactions) => 
        new(transactions);
}
