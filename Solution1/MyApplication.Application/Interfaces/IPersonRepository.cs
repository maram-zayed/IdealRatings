namespace MyApplication.Application.Services;

public interface IPersonRepository
{
    Task<IEnumerable<Domain.Entities.Person>> GetAllAsync(string? filterByName = null);
}