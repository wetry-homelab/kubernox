using Kubernox.Infrastructure.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IHostRepository
    {
        Task<IEnumerable<Host>> ReadAllHostAsync();
        Task<Guid?> InsertHostAsync(Host entity);
    }
}
