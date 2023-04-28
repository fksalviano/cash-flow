namespace Application.Domain;

public class Transaction
{    
    public Guid Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string Type { get; private set; }
    public decimal Value { get; private set; }

    public Transaction(Guid id, DateTime date, string description, string type, decimal value)
    {
        Id = id;
        Date = date;
        Description = description;
        Type = type;
        Value = value;
    }    
}
