using ApiTest.Model.Enum;

namespace ApiTest.Model;

public record TransactionResponse(string ReservationId, IntegrationTransactionStatusEnum Status, DateTime ConfirmedAt);