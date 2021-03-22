using AutoMapper;
using Domain.Entities;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;

namespace Application.Mappers
{
    public class DomainNameMapperProfile : Profile
    {
        public DomainNameMapperProfile()
        {
            CreateMap<DomainName, DomainNameItemResponse>();
            CreateMap<DomainNameCreateRequest, DomainName>();
        }
    }
}
