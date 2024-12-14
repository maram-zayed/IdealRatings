using Microsoft.EntityFrameworkCore;
using MyApplication.Application.Services;
using MyApplication.Domain.Entities;
using MyApplication.Infrastructure.Persistence;
using System;

namespace MyApplication.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;
    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Person>> GetAllAsync(string? filterByName = null)
    {
        var query = _context.Persons.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterByName))
        {
            query = query.Where(p => p.FirstName.Contains(filterByName) || p.LastName.Contains(filterByName));
        }

        return await query.ToListAsync();
    }
}
