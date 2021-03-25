using AutoMapper;
using Application.Entities;
using Infrastructure.Contracts.Response;

namespace Application.Mappers
{
    public class MetricMapperProfile : Profile
    {
        public MetricMapperProfile()
        {
            CreateMap<Metric, SimpleMetricItemResponse>()
                .ForMember(m => m.CpuValue, me => me.MapFrom(met => met.CpuValue))
                .ForMember(m => m.RamValue, me => me.MapFrom(met => met.MemoryValue))
                .ForMember(m => m.DateValue, me => me.MapFrom(met => met.Date));
        }
    }
}
