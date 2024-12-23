using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_finance.Models;
using my_finance.Services;

namespace my_finance.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PaymentsController: ControllerBase
{

    private readonly List<Payment> _payments = [];
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(Payment payment)
    {
        return Created("", await _paymentService.AddPayment(payment));
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AllPayments()
    {
        return Ok(await _paymentService.ListAll());
    }

    [HttpGet("/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthly(int month)
    {
        return Ok(await _paymentService.ListMonth(month));
    }

    [HttpGet("/total")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotal()
    {
        return Ok(await _paymentService.GetTotalLifetime());
    }

    [HttpGet("/total/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthTotal(int month)
    {
        return Ok(await _paymentService.GetMonthlyTotal(month));
    }

    [HttpGet("/total/currentMonth")]
    public async Task<IActionResult> GetCurrentMonthTotal()
    {
        return Ok(await _paymentService.GetMonthlyTotal(DateTime.Now.Month));
    }
}