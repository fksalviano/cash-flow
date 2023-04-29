using System.Data;
using Application.Domain;
using Application.UseCases.ListTransactions.Abstractions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ListTransactions.Repositories;

public class ListTransactionsRepository : IListTransactionsRepository
{
    private readonly ILogger<ListTransactionsRepository> _logger;
    private readonly IDbConnection _connection;

    public ListTransactionsRepository(ILogger<ListTransactionsRepository> logger, IDbConnection connection)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<Transaction>?> GetTransactionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var sql = @"select Id, Date, Description, Type, Value 
                        from [dbo].[Transaction]";

            return await _connection.QueryAsync<Transaction>(sql);            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Get Transactions");
            return null;
        }
    }
}
