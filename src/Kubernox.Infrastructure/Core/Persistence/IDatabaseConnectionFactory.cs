using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Core.Persistence
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();
    }

}
