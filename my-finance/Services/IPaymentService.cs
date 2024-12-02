using my_finance.Models;

namespace my_finance.Services;

public interface IPaymentService
{
 
    Task<decimal> AddPayment(Payment payment);
}