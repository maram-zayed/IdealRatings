namespace MyApplication.Application.Services;

public interface IPersonRepository
{
    //Task<IEnumerable<Domain.Entities.Person>> GetAllAsync();
    //Task<IEnumerable<Domain.Entities.Person>> GetFilteredAsync(string nameFilter);
    Task<IEnumerable<Domain.Entities.Person>> GetAllAsync(string? filterByName = null);
}