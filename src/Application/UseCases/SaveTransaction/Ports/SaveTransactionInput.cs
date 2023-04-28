namespace Application.UseCases.SaveTransaction.Ports;

public record SaveTransactionInput
(
    DateTime Date,
    string Description,
    string Type,
    decimal Value
);
