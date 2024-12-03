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

    public Task<List<Payment>> ListAll()
    {
        return Task.FromResult(_payments);
    }

    public Task<List<Payment>> ListMonth(int month)
    {
        return Task.FromResult(_payments.FindAll(p => p.PaymentDate.Month == month).ToList());
    }

    public Task<decimal> GetTotalLifetime()
    {
        return Task.FromResult(_payments.Sum(p => p.Amount));
    }
}