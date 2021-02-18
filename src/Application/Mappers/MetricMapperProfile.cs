using AutoMapper;
using Domain.Entities;
using Infrastructure.Contracts.Response;

namespace Application.Mappers
{
    public class MetricMapperProfile : Profile
    {
        public MetricMapperProfile()
        {
            CreateMap<Metric, SimpleMetricItemResponse>();
        }
    }
}
