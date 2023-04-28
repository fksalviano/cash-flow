using Application.Commons.Extensions;
using Application.UseCases.SaveTransaction.Abstractions;
using Application.UseCases.SaveTransaction.Ports;
using FluentValidation;

namespace Application.UseCases.SaveTransaction;

public class SaveTransactionUseCaseValidation : AbstractValidator<SaveTransactionInput>, ISaveTransactionUseCase
{    
    private readonly ISaveTransactionUseCase _useCase;
    private ISaveTransactionOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveTransactionOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveTransactionUseCaseValidation(ISaveTransactionUseCase useCase)
    {
        _useCase = useCase;

        RuleFor(input => input.Description)
            .NotEmpty()
            .WithMessage("Description is null or empty");

        RuleFor(input => input.Type)
            .Must(type => (new string[]{ "C", "D" }).Contains(type))
            .WithMessage("Type should be 'C' for Credit or 'D' for Debit");

        RuleFor(input => input.Value)
            .GreaterThan(0)
            .WithMessage("Value should be greater than 0");
    }

    public async Task ExecuteAsync(SaveTransactionInput input, CancellationToken cancellationToken)
    {
        var validation = await ValidateAsync(input, cancellationToken);
        if (!validation.IsValid)
        {
            _outputPort.Invalid(validation.ToResult());
            return;
        }
        
        await _useCase.ExecuteAsync(input, cancellationToken);
    }    
}
