using my_finance.Models;

namespace my_finance.Services;

public class PaymentService : IPaymentService
{

    private List<Payment> _payments = [];
    
    public Task<decimal> AddPayment(Payment payment)
    {
        _payments.Add(payment);
        return Task.FromResult(_payments.Sum(p => p.Amount));
    }
}