namespace Application.UseCases.GetDailyBalance.Domain;

public class DailyBalance
{
    public DateTime Date { get; set; }
    public decimal DayBalance { get; set; }
    public decimal CurrentBalance { get; set; }

    public DailyBalance() { } //TODO: check why it was necessary to dapper mapper work

    public DailyBalance(DateTime date, decimal dayBalance, decimal currentBalance)
    {
        Date = date;
        DayBalance = dayBalance;
        CurrentBalance = currentBalance;
    }
}
