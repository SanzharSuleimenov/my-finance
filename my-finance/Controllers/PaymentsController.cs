using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_finance.Models;

namespace my_finance.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PaymentsController: ControllerBase
{

    private readonly List<Payment> _payments = [];

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(Payment payment)
    {
        _payments.Add(payment);
        return Created("", _payments.Sum(p => p.Amount));
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AllPayments()
    {
        return Ok(_payments);
    }

    [HttpGet("/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthly(int month)
    {
        var payments = _payments.FindAll(p => p.PaymentDate.Month == month)
            .OrderByDescending(p => p.PaymentDate)
            .ToList();
        return Ok(payments);
    }

    [HttpGet("/total")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotal()
    {
        return Ok(_payments.Sum(p => p.Amount));
    }

    [HttpGet("/total/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthTotal(int month)
    {
        return Ok(_payments.FindAll(p => p.PaymentDate.Month == month).Sum(p => p.Amount));
    }
}