using Application.UseCases.GetDailyBalance.Abstractions;
using Application.UseCases.GetDailyBalance.Ports;
using Microsoft.AspNetCore.Mvc;
using static Application.Commons.Utils.ObjectResultUtils;

namespace Worker.Controllers.GetDailyBalance;

[ApiController]
[Route("balance")]
public class GetDailyBalanceController : ControllerBase, IGetDailyBalanceOutputPort
{
    private readonly IGetDailyBalanceUseCase _useCase;
    private IActionResult _viewModel = null!;

    public GetDailyBalanceController(IGetDailyBalanceUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    /// <summary>
    /// Get consolidated balance by day.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetDailyBalance")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDailyBalanceResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetDailyBalanceResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetDailyBalanceResponse))]
    public async Task<IActionResult> GetDailyBalance(CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(cancellationToken);

        return _viewModel;
    }

    void IGetDailyBalanceOutputPort.Ok(GetDailyBalanceOutput output) =>
        _viewModel = Ok(GetDailyBalanceResponse.Success(output));

    void IGetDailyBalanceOutputPort.NotFound() =>
        _viewModel = NotFound(GetDailyBalanceResponse.Error("Not found Transactions"));

    void IGetDailyBalanceOutputPort.Error(string errorMessage) =>
        _viewModel = InternalServerError(GetDailyBalanceResponse.Error(errorMessage));
}
