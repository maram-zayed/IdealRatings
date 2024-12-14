using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApplication.Application.Common.Models;
using MyApplication.Application.Person.DTOs;
using MyApplication.Application.Queries;

namespace MyApplication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<PersonDto>>>> GetPersons([FromQuery] string? filterByName)
    {
       return await _mediator.Send(new GetPersonsQuery { Filter = filterByName });
    }
}
