namespace Application.UseCases.GetDailyBalance.Domain;

public class DailyBalance
{
    public DateTime Date { get; private set; }
    public decimal DayBalance { get; private set; }
    public decimal CurrentBalance { get; private set; }

    public DailyBalance(DateTime date, decimal dayBalance, decimal currentBalance)
    {
        Date = date;
        DayBalance = dayBalance;
        CurrentBalance = currentBalance;
    }

    public void SetCurrentBalance(decimal value) =>
        CurrentBalance = value;
}
