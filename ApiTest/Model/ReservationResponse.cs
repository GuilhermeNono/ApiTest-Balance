namespace ApiTest.Model;

public record ReservationResponse(
    string ReservationId,
    string Identifier,
    DateTime RegisteredAt,
    decimal Amount,
    decimal Balance);