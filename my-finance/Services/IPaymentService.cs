using my_finance.Models;

namespace my_finance.Services;

public interface IPaymentService
{
 
    Task<decimal> AddPayment(Payment payment);
    Task<List<Payment>> ListAll();
    Task<List<Payment>> ListMonth(int month);
    Task<decimal> GetTotalLifetime();
}