using System;
using System.Collections.Generic;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using my_finance.Controllers;
using my_finance.Models;
using Xunit;

namespace my_finance.Tests.Controllers;

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
}