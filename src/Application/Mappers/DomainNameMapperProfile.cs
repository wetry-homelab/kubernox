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
            CreateMap<DomainName, DomainNameItemResponse>()
                .ForMember(d => d.RootDomain, (e) => e.MapFrom(p => p.Value));
            CreateMap<DomainNameCreateRequest, DomainName>();
        }
    }
}
