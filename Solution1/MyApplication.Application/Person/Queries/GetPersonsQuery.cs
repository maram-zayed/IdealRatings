using MyApplication.Application.Person.DTOs;
using MediatR;
using MyApplication.Application.Queries;
using MyApplication.Application.Services;
using AutoMapper;
using MyApplication.Application.Common.Models;

namespace MyApplication.Application.Queries
{
    public class GetPersonsQuery : IRequest<BaseResponse<IEnumerable<PersonDto>>>
    {
        public string? Filter { get; set; }
    }
}

namespace MyApplication.Application.Handlers
{
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, BaseResponse<IEnumerable<PersonDto>>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPersonsQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<PersonDto>>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAllAsync(request.Filter);
            var results = _mapper.Map<IEnumerable<PersonDto>>(persons);

            if (results.Count() == 0)
            {
                return new BaseResponse<IEnumerable<PersonDto>> { Data = results, Message = "No data found" };
            }

            return new BaseResponse<IEnumerable<PersonDto>> { Data = results , Message = "Success" };
        }
    }
}

