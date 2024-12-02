using System;
using System.Collections.Generic;
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
    public async void ShouldReturn_Created201_AndTotalSum()
    {
        // given
        Payment payment = new("01", 2000, "AED", "HSBC", DateTime.Now);

        // when
        var totalExpenses = await _controller.Add(payment);

        // then
        totalExpenses.Should().BeOfType<CreatedResult>();
        var createdResult = (CreatedResult)totalExpenses;
        createdResult.Value.Should().BeOfType<decimal>();
        createdResult.Value.Should().Be(2000);
    }

    [Fact]
    public async void ShouldReturn_Ok200_ListOfPayments()
    {
        // given
        List<Payment> paymentsList = [];

        // when
        var result = await _controller.AllPayments();

        //then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<List<Payment>>();
        resultValue.Value.Should().BeEquivalentTo(paymentsList);
    }

    [Fact]
    public async void ShouldReturn_Ok200_ListOfCurrentMonthPayments()
    {
        // given
        List<Payment> paymentsList = [];

        // when
        var result = await _controller.GetMonthly(DateTime.Now.Month);

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<List<Payment>>();
        resultValue.Value.Should().BeEquivalentTo(paymentsList);
    }

    [Fact]
    public async void ShouldReturn_Ok200_TotalLifetime()
    {
        // given & when
        var result = await _controller.GetTotal();

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(0);
    }

    [Fact]
    public async void ShouldReturn_Ok200_MonthTotal()
    {
        // given & when
        var result = await _controller.GetMonthTotal(DateTime.Now.Month);

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(0);
    }

    [Fact]
    public async void ShouldReturn_Ok200_CurrentMonthTotal()
    {
        // given & when
        var result = await _controller.GetCurrentMonthTotal();

        // then
        result.Should().BeOfType<OkObjectResult>();
        var resultValue = (OkObjectResult)result;
        resultValue.Value.Should().BeOfType<decimal>();
        resultValue.Value.Should().Be(0);
    }
}
