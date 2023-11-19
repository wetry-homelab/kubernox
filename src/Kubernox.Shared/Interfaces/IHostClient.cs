using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kubernox.Shared.Contracts.Response;

namespace Kubernox.Shared.Interfaces
{
    public interface IHostClient
    {
        Task<List<HostItemResponse>> GetHostsAsync();
    }
}
