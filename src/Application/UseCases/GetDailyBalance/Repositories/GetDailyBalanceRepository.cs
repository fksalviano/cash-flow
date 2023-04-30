using System.Data;
using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Domain;
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
