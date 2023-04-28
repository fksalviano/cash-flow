using Application.Domain;

namespace Application.UseCases.SaveTransaction.Ports;

public record SaveTransactionOutput
(
    Transaction Transaction
);
