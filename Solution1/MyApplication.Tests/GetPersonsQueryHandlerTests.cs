using AutoMapper;
using FluentAssertions;
using Moq;
using MyApplication.Application.Handlers;
using MyApplication.Application.Person.DTOs;
using MyApplication.Application.Queries;
using MyApplication.Application.Services;
using MyApplication.Domain.Entities;

namespace MyApplication.Tests;

public class GetPersonsQueryHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetPersonsQueryHandler _handler;

    public GetPersonsQueryHandlerTests()
    {
        _mockRepository = new Mock<IPersonRepository>();
        _mockMapper = new Mock<IMapper>();

        _handler = new GetPersonsQueryHandler(_mockRepository.Object, _mockMapper.Object);
    }
    [Fact]
    public async Task Handle_ReturnsData_WhenPersonsExist()
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "Ahmed", LastName = "Mohammed", TelephoneCode = "20", TelephoneNumber = "010334445", Address = "10 Road Street", Country = "Egypt" },
            new Person { Id = 3, FirstName = "Amr", LastName = "Bahy", TelephoneCode = "20", TelephoneNumber = "123409586", Address = "12 Road Street", Country = "Egypt" },
        };

        var personsDto = new List<PersonDto>
        {
            new PersonDto { FirstName = "Ahmed", LastName = "Mohammed", TelephoneCode = "20", TelephoneNumber = "010334445", Address = "123 Main St", Country = "Egypt" },
            new PersonDto { FirstName = "Amr", LastName = "Bahy", TelephoneCode = "20", TelephoneNumber = "123409586", Address = "12 Road Street", Country = "Egypt" },
        };

        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<string?>())).ReturnsAsync(persons);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PersonDto>>(persons)).Returns(personsDto);

        var query = new GetPersonsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Data.Should().HaveCount(2);
        result.Data.Should().BeEquivalentTo(personsDto);
        result.Message.Should().Be("Success");
    }

    [Fact]
    public async Task Handle_ReturnsNoData_WhenNoPersonsExist()
    {
        _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<string?>())).ReturnsAsync(new List<Person>());
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PersonDto>>(It.IsAny<List<Person>>())).Returns(new List<PersonDto>());

        var query = new GetPersonsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Data.Should().BeEmpty();
        result.Message.Should().Be("No data found");
    }
}
