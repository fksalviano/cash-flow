using System.Data;
using Application.UseCases.GetDailyBalance.Domain;
using Application.UseCases.GetDailyBalance.Abstractions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetDailyBalance.Repositories;

public class GetDailyBalanceRepository : IGetDailyBalanceRepository
{
    private readonly ILogger<GetDailyBalanceRepository> _logger;
    private readonly IDbConnection _connection;

    public GetDailyBalanceRepository(ILogger<GetDailyBalanceRepository> logger, IDbConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public async Task<IEnumerable<DailyBalance>?> GetDailyBalancesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var sql = @"select 
                            cast(cast([Date] as DATE) as DATETIME) as [Date],
                            sum(case when [Type] = 'C' then [Value] else [Value] * -1 end) as [DayBalance]
                        from [dbo].[Transaction]
                        group by cast(cast([Date] as DATE) as DATETIME)
                        order by 1";

            decimal currentBalance = 0;

            return await _connection.QueryAsync<DailyBalance, DailyBalance, DailyBalance>(sql, 
                (daily, _) => 
                {
                    currentBalance += daily.DayBalance;
                    return new DailyBalance(daily.Date, daily.DayBalance, currentBalance);
                }, 
                splitOn: "DayBalance");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Get Balances");
            return null;
        }
    }
}
