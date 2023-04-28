using System.Data;
using Application.Domain;
using Application.UseCases.SaveTransaction.Abstractions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.SaveTransaction.Repositories;

public class SaveTransactionRepository : ISaveTransactionRepository
{
    private readonly ILogger<SaveTransactionRepository> _logger;
    private readonly IDbConnection _connection;

    public SaveTransactionRepository(ILogger<SaveTransactionRepository> logger, IDbConnection connection)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<bool> SaveAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            var sql = @"insert into [dbo].[Transaction] (Id, Date, Description, Type, Value)
                        values (@Id, @Date, @Description, @Type, @Value)";

            await _connection.ExecuteAsync(sql, transaction);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Save Transaction");
            return false;
        }
    }
}
