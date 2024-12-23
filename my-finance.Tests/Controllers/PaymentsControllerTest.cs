using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using my_finance.Controllers;
using my_finance.Models;
using my_finance.Services;
using Xunit;

namespace my_finance.Tests.Controllers;

[TestSubject(typeof(PaymentsController))]
public class PaymentsControllerTest
{
    private readonly PaymentsController _controller;
    private readonly Mock<IPaymentService> _stubPaymentService;

    public PaymentsControllerTest()
    {
        _stubPaymentService = new Mock<IPaymentService>();
        _controller = new PaymentsController(_stubPaymentService.Object);
    }

    [Fact]
    public async Task ShouldReturn_Created201_AndTotalSum()
    {
        // given
        Payment payment = Instant();
        _stubPaymentService.Setup(s => s.AddPayment(payment)).ReturnsAsync(10000);

        // when
        var totalExpenses = await _controller.Add(payment);

        // then
        totalExpenses.Should().BeOfType<CreatedResult>();
        var createdResult = (CreatedResult)totalExpenses;
        createdResult.Value.Should().BeOfType<decimal>();
        createdResult.Value.Should().Be(10000);
    }

    [Fact]
    public async Task ShouldReturn_Ok200_ListOfPayments()
    {
        // given
        List<Payment> paymentsList = [Instant(), OldPayment()];
        _stubPaymentService.Setup(s => s.ListAll()).ReturnsAsync(paymentsList);

        // when
        var result = await _controller.AllPayments();

        //then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<List<Payment>>();
        resultValue.Value.Should().BeEquivalentTo(paymentsList);
    }

    [Fact]
    public async Task ShouldReturn_Ok200_ListOfSpecificMonthPayments()
    {
        // given
        List<Payment> paymentsList = [Instant(), OldPayment()];
        _stubPaymentService.Setup(s => s.ListMonth(DateTime.Now.Month)).ReturnsAsync(paymentsList);

        // when
        var result = await _controller.GetMonthly(DateTime.Now.Month);

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<List<Payment>>();
        resultValue.Value.Should().BeEquivalentTo(paymentsList);
    }

    [Fact]
    public async Task ShouldReturn_Ok200_TotalLifetime()
    {
        // given
        const decimal totalExpenses = 30000;
        _stubPaymentService.Setup(s => s.GetTotalLifetime())
            .ReturnsAsync(totalExpenses);
        
        // when
        var result = await _controller.GetTotal();

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(totalExpenses);
    }

    [Fact]
    public async Task ShouldReturn_Ok200_MonthTotal()
    {
        // given
        const decimal monthlyExpenses = 26000;
        _stubPaymentService.Setup(s => s.GetMonthlyTotal(DateTime.Now.Month))
            .ReturnsAsync(monthlyExpenses);
        
        // when
        var result = await _controller.GetMonthTotal(DateTime.Now.Month);

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(monthlyExpenses);
    }

    [Fact]
    public async Task ShouldReturn_Ok200_CurrentMonthTotal()
    {
        // given
        const decimal monthlyExpenses = 26000;
        _stubPaymentService.Setup(s => s.GetMonthlyTotal(DateTime.Now.Month))
            .ReturnsAsync(monthlyExpenses);
        
        // when
        var result = await _controller.GetCurrentMonthTotal();

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(monthlyExpenses);
    }

    private static Payment Instant()
    {
        return new Payment("01", 2000, "AED", "HSBC", DateTime.Now);
    }

    private static Payment OldPayment()
    {
        return new("02", 2000, "AED", "HSBC", DateTime.Now.Subtract(TimeSpan.FromDays(1)));
    }
}
