using MyApplication.Application.Person.DTOs;

namespace MyApplication.Application.Mapping;

public partial class AutoMapperConfig
{
    public void MapPerson()
    {
        CreateMap<Domain.Entities.Person, PersonDto>()
            .ReverseMap();
    }
}