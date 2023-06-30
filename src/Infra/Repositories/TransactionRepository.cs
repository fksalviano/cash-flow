using Dapper;
using System.Data;
using Domain.Models;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Infra.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {        
        private readonly ILogger<TransactionRepository> _logger;
        private readonly IDbConnection _connection;

        public TransactionRepository(ILogger<TransactionRepository> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public async Task<IEnumerable<Transaction>?> GetTransactionsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var sql = @"select [Id], [Date], [Description], [Type], [Value]
                            from [dbo].[Transaction]";

                return await _connection.QueryAsync<Transaction>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to Get Transactions");
                return null;
            }
        }

        public async Task<bool> SaveTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
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

        public async Task<IEnumerable<DailyBalance>?> GetDailyBalancesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var sql = @"select
                                cast(cast([Date] as DATE) as DATETIME) as [Date],
                                sum(case when [Type] = 'C' then [Value] else [Value] * -1 end) as [DayBalance],
                                0.00 as [CurrentBalance]
                            from [dbo].[Transaction]
                            group by cast(cast([Date] as DATE) as DATETIME)
                            order by 1";

                return await _connection.QueryAsync<DailyBalance>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to Get Balances");
                return null;
            }
        }
    }
}