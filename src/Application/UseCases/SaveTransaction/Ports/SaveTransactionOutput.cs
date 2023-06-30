using Domain.Models;

namespace Application.UseCases.SaveTransaction.Ports;

public record SaveTransactionOutput
(
    Transaction Transaction
);
