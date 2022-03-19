using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Core.Exceptions;
using Kubernox.Infrastructure.Helpers;
using Kubernox.Infrastructure.Infrastructure.Entities;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Business
{
    public class IdentityBusiness : IIdentityBusiness
    {
        private readonly IUserRepository userRepository;
        private readonly IMemoryCache memoryCache;

        public IdentityBusiness(IUserRepository userRepository, IMemoryCache memoryCache)
        {
            this.userRepository = userRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<AuthenticationResponse> AuthenticateUserAsync(AuthenticationRequest request)
        {
            var dbUser = await userRepository.ReadUserAsync(new User()
            {
                Username = request.Username
            });

            if (dbUser == null)
                throw new NotFoundException($"User not found {request.Username}.");

            if (!string.Equals(EncryptPassword(request.Password), dbUser.Password, StringComparison.OrdinalIgnoreCase))
                throw new ForbidException("Unable to identity user.");

            return new AuthenticationResponse()
            {
                Id = $"{dbUser.Id}",
                PasswordExpire = dbUser.PasswordExpireDate < DateTime.UtcNow,
                PasswordToken = dbUser.PasswordExpireDate < DateTime.UtcNow ? await GenerateUpdatePasswordToken(dbUser.Id, dbUser.Username) : null,
                Username = request.Username,
                Token = JwtTokenGenerator.GenerateToken(dbUser.Id, dbUser.Username)
            };
        }

        public async Task<AuthenticationResponse> UpdatePasswordAsync(PasswordUpdateRequest request)
        {
            var cacheKey = $"passwordToken_{request.Id}_{request.Username}";

            if (!memoryCache.TryGetValue(cacheKey, out string token))
                throw new NotFoundException("No token found for user.");

            if (!string.Equals(request.PasswordToken, token, StringComparison.OrdinalIgnoreCase))
            {
                memoryCache.Remove(cacheKey);
                throw new ForbidException("Invalid token.");
            }

            var encryptNewPassword = EncryptPassword(request.Password);
            var dbUser = await userRepository.ReadUserAsync(new User()
            {
                Username = request.Username
            });

            if (dbUser == null)
                throw new NotFoundException($"User not found {request.Username}.");

            dbUser.Password = encryptNewPassword;
            dbUser.PasswordExpireDate = DateTime.UtcNow.AddDays(365);

            if (await userRepository.UpdateUserAsync(dbUser) < 1)
                throw new Exception("Error on update.");

            return await AuthenticateUserAsync(new AuthenticationRequest()
            {
                Username = request.Username,
                Password = request.Password,
            });
        }

        private async Task<string> GenerateUpdatePasswordToken(Guid userId, string username)
        {
            var cacheKey = $"passwordToken_{userId}_{username}";
            var passwordToken = RandomString(64);

            if (!memoryCache.TryGetValue(cacheKey, out _))
            {
                memoryCache.Remove(cacheKey);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            memoryCache.Set(cacheKey, passwordToken, cacheEntryOptions);

            return passwordToken;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz@-_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
