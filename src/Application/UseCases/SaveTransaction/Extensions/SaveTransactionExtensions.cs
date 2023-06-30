using Domain.Models;
using Application.UseCases.SaveTransaction.Ports;

namespace Application.UseCases.SaveTransaction.Extensions;

public static class SaveTransactionExtensions
{
    public static Transaction ToTransaction(this SaveTransactionInput input) => new 
    (
        Guid.NewGuid(), 
        input.Date, 
        input.Description, 
        input.Type, 
        input.Value
    );

    public static SaveTransactionOutput ToOutput(this Transaction transaction) => new(transaction);
}
