using Application.UseCases.ListTransactions.Abstractions;
using Application.UseCases.ListTransactions.Ports;
using Microsoft.AspNetCore.Mvc;
using static Application.Commons.Utils.ObjectResultUtils;

namespace API.Controllers.ListTransactions;

[ApiController]
[Route("transaction")]
public class ListTransactionsController : ControllerBase, IListTransactionsOutputPort
{
    private readonly IListTransactionsUseCase _useCase;
    private IActionResult _viewModel = null!;

    public ListTransactionsController(IListTransactionsUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    /// <summary>
    /// Get all registered Transactions
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetTransactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListTransactionsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ListTransactionsResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ListTransactionsResponse))]
    public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(cancellationToken);

        return _viewModel;
    }

    void IListTransactionsOutputPort.Ok(ListTransactionsOutput output) =>
        _viewModel = Ok(ListTransactionsResponse.Success(output));

    void IListTransactionsOutputPort.NotFound() =>
        _viewModel = NotFound(ListTransactionsResponse.Error("Not found Transactions"));

    void IListTransactionsOutputPort.Error(string errorMessage) =>
        _viewModel = InternalServerError(ListTransactionsResponse.Error(errorMessage));
}
