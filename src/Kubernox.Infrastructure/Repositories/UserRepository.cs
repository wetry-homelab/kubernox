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
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly ILogger<UserRepository> logger;

        private const string SQL_QUERY_READALL = "SELECT * FROM [User]";
        private const string SQL_QUERY_READONE = "SELECT * FROM [User] WHERE Id = @Id OR [Username] = @Username";
        private const string SQL_QUERY_UPDATE = "UPDATE [User] SET [Password] = @Password, PasswordExpireDate = @PasswordExpireDate WHERE Id = @Id;";


        public UserRepository(IDatabaseConnectionFactory databaseConnectionFactory, ILogger<UserRepository> logger)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            this.logger = logger;
        }

        public Task<IEnumerable<User>> ReadAllUserAsync()
        {
            try
            {
                var conn = databaseConnectionFactory.GetConnection();
                return conn.QueryAsync<User>(SQL_QUERY_READALL);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseReadAllFailed, e, "Error on read all user.");
            }

            return null;
        }

        public Task<User> ReadUserAsync(User query)
        {
            try
            {
                var conn = databaseConnectionFactory.GetConnection();
                return conn.QueryFirstOrDefaultAsync<User>(SQL_QUERY_READONE, query);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseReadOneFailed, e, "Error on read one user.", query);
            }

            return null;
        }

        public Task<int> UpdateUserAsync(User entity)
        {
            try
            {
                var connection = databaseConnectionFactory.GetConnection();
                return connection.ExecuteAsync(SQL_QUERY_UPDATE, entity);
            }
            catch (Exception e)
            {
                logger.LogError(ExceptionCode.DatabaseUpdateFailed, e, "Error on update user.", entity);
            }

            return Task.FromResult(-1);
        }
    }
}
