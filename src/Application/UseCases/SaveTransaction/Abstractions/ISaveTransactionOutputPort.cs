using Application.Commons.Domain;
using Application.UseCases.SaveTransaction.Ports;

namespace Application.UseCases.SaveTransaction.Abstractions;

public interface ISaveTransactionOutputPort
{
    void Ok(SaveTransactionOutput output);
    void Invalid(Result result);
    void Error(string errorMessage);
}