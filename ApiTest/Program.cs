using ApiTest.Model;
using ApiTest.Model.Enum;
using ApiTest.Model.Objects;
using ApiTest.Model.Static;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/balance", ([FromBody]BalanceRequest request) =>
    {
        var user = UserWrapper.Users.Find(x => x.Identifier.Equals(request.Identifier));

        if (user is null)
            return null;
        
        return new BalanceResponse(user.Identifier, user.Transactions.Sum(x => x.Amount));
    })
    .WithName("balance")
    .WithOpenApi();

app.MapPost("/reservations", ([FromBody]ReservationRequest request) =>
    {
        var user = UserWrapper.Users.Find(x => x.Identifier.Equals(request.Identifier));

        if (user is null)
            return null;

        var balance = user.Transactions.Sum(x => x.Amount);
        
        if (balance < request.Amount)
            throw new ApplicationException("Insufficient balance");

        var transaction = new Transaction(Guid.NewGuid(), request.Amount * -1, true);
        
        user.Transactions.Add(transaction);
        
        return new ReservationResponse(transaction.Id.ToString(), request.Identifier, transaction.RegisteredAt, request.Amount, user.Transactions.Sum(x => x.Amount));
    })
    .WithName("reservations")
    .WithOpenApi();

app.MapPost("/reservations/confirm", ([FromBody]TransactionRequest request) =>
    {
        var user = UserWrapper.Users.Find(x => x.Identifier.Equals(request.Identifier));

        if (user is null)
            return null;

        var transaction = user.Transactions.Find(x => x.Id.ToString().Equals(request.IdReservation, StringComparison.OrdinalIgnoreCase));

        if(transaction is null)
            throw new ApplicationException("Transaction not found");
        
        transaction.IsBlocked = false;
        transaction.RegisteredAt = DateTime.Now;
        
        return new TransactionResponse(user.Identifier, IntegrationTransactionStatusEnum.Confirmed, transaction.RegisteredAt);
    })
    .WithName("reservation Confirm")
    .WithOpenApi();

app.MapPost("/reservations/cancel", ([FromBody]TransactionRequest request) =>
    {
        var user = UserWrapper.Users.Find(x => x.Identifier.Equals(request.Identifier));

        if (user is null)
            return null;
        
        var transaction = user.Transactions.Find(x => x.Id.ToString().Equals(request.IdReservation, StringComparison.OrdinalIgnoreCase));

        if(transaction is null)
            throw new ApplicationException("Transaction not found");
        
        user.Transactions.Remove(transaction);
        
        return new TransactionResponse(user.Identifier, IntegrationTransactionStatusEnum.Cancelled, DateTime.Now);
    })
    .WithName("reservation Cancel")
    .WithOpenApi();

app.Run();