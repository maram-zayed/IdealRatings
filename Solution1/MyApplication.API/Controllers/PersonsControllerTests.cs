﻿using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApplication.Application.Common.Models;
using MyApplication.Application.Person.DTOs;
using MyApplication.Application.Queries;
using Xunit;

namespace MyApplication.API.Controllers;

public class PersonsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly PersonsController _controller;

    public PersonsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new PersonsController(_mockMediator.Object);
    }

    [Fact]
    public async Task GetPersons_ReturnsOkWithData_WhenPersonsExist()
    {
        var personsDto = new List<PersonDto>
            {
                new PersonDto { FirstName = "Ahmed", LastName = "Mohammed", TelephoneCode = "20", TelephoneNumber = "010334445", Address = "123 Main St", Country = "Egypt" },
                new PersonDto { FirstName = "Amr", LastName = "Bahy", TelephoneCode = "20", TelephoneNumber = "123409586", Address = "12 Road Street", Country = "Egypt" },
            };

        var response = new BaseResponse<IEnumerable<PersonDto>> { Data = personsDto, Message = "Success" };

        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPersonsQuery>(), default))
                     .ReturnsAsync(response);

        var result = await _controller.GetPersons(null);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>(); 

        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();

        var responseData = okResult.Value as BaseResponse<IEnumerable<PersonDto>>;
        responseData.Should().NotBeNull();
        responseData.Data.Should().HaveCount(2);
        responseData.Message.Should().Be("Success");
    }

    [Fact]
    public async Task GetPersons_ReturnsOkWithNoData_WhenNoPersonsExist()
    {
        var response = new BaseResponse<IEnumerable<PersonDto>> { Data = new List<PersonDto>(), Message = "No data found" };

        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPersonsQuery>(), default))
                     .ReturnsAsync(response);

        var result = await _controller.GetPersons(null);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();  

        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();

        var responseData = okResult.Value as BaseResponse<IEnumerable<PersonDto>>;
        responseData.Should().NotBeNull();
        responseData.Data.Should().BeEmpty();
        responseData.Message.Should().Be("No data found");
    }
}
