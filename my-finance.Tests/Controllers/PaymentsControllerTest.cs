using System;
using FluentAssertions;
using JetBrains.Annotations;
using my_finance.Controllers;
using my_finance.Models;
using Xunit;

namespace my_finance.Tests.Controllers;

// add payment
// list all payments
// list current month payments
// list payments history
// get total history
// get total in month
// get total in current month
[TestSubject(typeof(PaymentsController))]
public class PaymentsControllerTest
{
 
    private readonly PaymentsController _controller;

    public PaymentsControllerTest()
    {
        _controller = new PaymentsController();
    }

    [Fact]
    public void PaymentController_ShouldReturn_Ok()
    {
        // given
        Payment payment = new("01", 2000, "AED", "HSBC", DateTime.Now);
        // when
        var totalExpenses = _controller.Add(payment).Result;
        // then
        totalExpenses.Should().NotBeNull();
    }
}