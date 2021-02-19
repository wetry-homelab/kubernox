using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Application.Messages;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ClusterMapperProfile : Profile
    {
        public ClusterMapperProfile()
        {
            CreateMap<ClusterCreateRequest, Cluster>();
            CreateMap<ClusterUpdateRequest, Cluster>();
            CreateMap<Cluster, ClusterItemResponse>();
            CreateMap<Cluster, ClusterDetailsResponse>()
                     .ForMember(m => m.Disk, me => me.MapFrom(met => met.Storage));
            CreateMap<Cluster, ClusterCreateMessage>();
            CreateMap<ClusterNode, ClusterNodeDetailsResponse>();
            CreateMap<ClusterUpdateRequest, ClusterUpdateMessage>();
        }
    }
}
