using Dapper;
using DapperExtensions;
using Kubernox.Infrastructure.Core.Exceptions;
using Kubernox.Infrastructure.Core.Persistence;
using Kubernox.Infrastructure.Infrastructure.Entities;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Repositories
{
    public class HostRepository : IHostRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly ILogger<HostRepository> logger;

        private const string SQL_QUERY_READALL = "SELECT * FROM [Host]";

        public HostRepository(IDatabaseConnectionFactory databaseConnectionFactory, ILogger<HostRepository> logger)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            this.logger = logger;
        }

        public Task<IEnumerable<Host>> ReadAllHostAsync()
        {
            try
            {
                var conn = databaseConnectionFactory.GetConnection();
                return conn.QueryAsync<Host>(SQL_QUERY_READALL);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseReadAllFailed, e, "Error on read all host.");
            }

            return null;
        }

        public async Task<Guid?> InsertHostAsync(Host entity)
        {
            try
            {
                var connection = databaseConnectionFactory.GetConnection();
                return await connection.InsertAsync(entity);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseInsertFailed, e, "Error on insert new host.");
            }

            return null;
        }
    }
}
