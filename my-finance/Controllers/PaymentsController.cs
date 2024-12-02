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
}