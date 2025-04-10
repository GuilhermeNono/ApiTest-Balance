namespace ApiTest.Model.Objects;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }

    public List<Transaction> Transactions { get; set; } =
    [
        new Transaction(Guid.NewGuid(), decimal.Parse(new Random().NextInt64(154, 6583).ToString())),
        new Transaction(Guid.NewGuid(), decimal.Parse(new Random().NextInt64(154, 6583).ToString())),
        new Transaction(Guid.NewGuid(), decimal.Parse(new Random().NextInt64(154, 6583).ToString()))
    ];

    public User(long id, string name, string identifier)
    {
        Id = id;
        Name = name;
        Identifier = identifier;
    }
}