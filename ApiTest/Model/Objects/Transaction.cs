namespace ApiTest.Model.Objects;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.Now;

    public Transaction(Guid id, decimal amount, bool isBlocked = false)
    {
        Id = id;
        Amount = amount;
        IsBlocked = isBlocked;
    }
}