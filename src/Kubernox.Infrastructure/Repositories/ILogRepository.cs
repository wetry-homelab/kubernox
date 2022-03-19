using Dapper;
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
    public class LogRepository : ILogRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly ILogger<LogRepository> logger;

        private const string SQL_QUERY_READALL = "SELECT * FROM[KUBERNOX].[dbo].[Log] ORDER BY[TimeStamp] DESC OFFSET #START# ROWS FETCH NEXT #MAX# ROWS ONLY";
        private const string SQL_QUERY_COUNTALL = "SELECT * FROM[KUBERNOX].[dbo].[Log]";

        public LogRepository(IDatabaseConnectionFactory databaseConnectionFactory, ILogger<LogRepository> logger)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            this.logger = logger;
        }

        public Task<IEnumerable<Log>> ReadLogsAsync(int start = 0, int max = 100)
        {
            try
            {
                var query = SQL_QUERY_READALL.Replace("#START#", $"{start}").Replace("#MAX#", $"{max}");
                var conn = databaseConnectionFactory.GetConnection();
                return conn.QueryAsync<Log>(query);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseReadAllFailed, e, "Error on read all host.");
            }

            return null;
        }

        public Task<int> CountLogEntry()
        {
            try
            {
                var conn = databaseConnectionFactory.GetConnection();
                return conn.QueryFirstAsync<int>(SQL_QUERY_COUNTALL);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseReadAllFailed, e, "Error on read all host.");
            }

            return Task.FromResult(0);
        }
    }
}
