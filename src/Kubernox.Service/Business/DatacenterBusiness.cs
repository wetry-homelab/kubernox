using Application.Interfaces;
using AutoMapper;
using Infrastructure.Contracts.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class DatacenterBusiness : IDatacenterBusiness
    {
        private readonly ILogger<DatacenterBusiness> logger;
        private readonly IDatacenterRepository datacenterRepository;
        private readonly IMapper mapper;

        public DatacenterBusiness(ILogger<DatacenterBusiness> logger, IDatacenterRepository datacenterRepository, IMapper mapper)
        {
            if (datacenterRepository == null)
                throw new ArgumentNullException(nameof(datacenterRepository));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger;
            this.datacenterRepository = datacenterRepository;
            this.mapper = mapper;
        }

        public async Task<DatacenterNodeResponse[]> GetDatacenter()
        {
            var nodes = await datacenterRepository.ReadsAsync();

            return nodes.Select(node => mapper.Map<DatacenterNodeResponse>(node)).ToArray();
        }

        public async Task<DatacenterNodeResponse> GetDatacenterNode(int id)
        {
            var node = await datacenterRepository.ReadAsync(n => n.Id == id);
            if (node == null)
                return null;
            return mapper.Map<DatacenterNodeResponse>(node);
        }
    }
}
