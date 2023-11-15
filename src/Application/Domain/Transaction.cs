using System.ComponentModel;
using EnumsNET;

namespace Application.Domain;

public class Transaction
{    
    public Guid Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string? Type => MovementType.AsString(EnumFormat.Description);
    public MovementType MovementType { get; private set; }
    public decimal Value { get; private set; }

    public Transaction(Guid id, DateTime date, string description, MovementType type, decimal value)
    {
        Id = id;
        Date = date;
        Description = description;
        MovementType = type;
        Value = value;
    }    
}