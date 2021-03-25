using AutoMapper;
using Application.Entities;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;

namespace Application.Mappers
{
    public class DomainNameMapperProfile : Profile
    {
        public DomainNameMapperProfile()
        {
            CreateMap<Domain, DomainItemResponse>()
                .ForMember(d => d.RootDomain, (e) => e.MapFrom(p => p.Value));
            CreateMap<DomainNameCreateRequest, Domain>();
        }
    }
}
