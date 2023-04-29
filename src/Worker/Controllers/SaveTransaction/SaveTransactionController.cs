using Application.Commons.Domain;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Ports;
using Microsoft.AspNetCore.Mvc;
using static Application.Commons.Utils.ObjectResultUtils;

namespace Worker.Controllers.SaveTransaction;

[ApiController]
[Route("transaction")]
public class SaveTransactionController : ControllerBase, ISaveTransactionOutputPort
{
    private readonly ISaveTransactionUseCase _useCase;
    private IActionResult _viewModel = null!;

    public SaveTransactionController(ISaveTransactionUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    /// <summary>
    /// Saves a Credit or Debit Transaction
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost(Name = "SaveTransaction")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SaveTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(SaveTransactionResponse))]
    public async Task<IActionResult> SaveTransaction([FromBody] SaveTransactionInput input, CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(input, cancellationToken);

        return _viewModel;
    }

    void ISaveTransactionOutputPort.Ok(SaveTransactionOutput output) =>
        _viewModel = Ok(SaveTransactionResponse.Success(output));

    void ISaveTransactionOutputPort.Invalid(Result result) =>
        _viewModel = BadRequest(SaveTransactionResponse.Error(result.ErrorMessage));

    void ISaveTransactionOutputPort.Error(string errorMessage) =>
        _viewModel = InternalServerError(SaveTransactionResponse.Error(errorMessage));
}
