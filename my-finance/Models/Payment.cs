namespace my_finance.Models;

public record Payment(string Id, decimal Amount, string Currency, string PaymentMethod, DateTime PaymentDate);