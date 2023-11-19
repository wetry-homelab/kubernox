using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

namespace Kubernox.WebUi.Core
{
    public class KubernoxAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public KubernoxAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await localStorageService.GetItemAsStringAsync("access_token");

                if (string.IsNullOrWhiteSpace(savedToken))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken.Replace("\"", ""));
                return new AuthenticationState(await ExtractClaims());
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task MarkUserAsAuthenticated()
        {
            var authenticatedUser = await ExtractClaims();
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOut()
        {
            await localStorageService.ClearAsync();
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task<ClaimsPrincipal> ExtractClaims()
        {

            var savedToken = await localStorageService.GetItemAsStringAsync("access_token");
            if (!string.IsNullOrEmpty(savedToken))
            {
                var user = ParseClaimsFromJwt(savedToken);
                var username = user.FirstOrDefault(f => f.Type == ClaimTypes.Name)?.Value;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken.Replace("\"", ""));

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, username)
                };

                foreach (var r in user.Where(f => f.Type == ClaimTypes.Role).ToList())
                    claims.Add(r);

                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                return authenticatedUser;
            }

            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            if (!string.IsNullOrEmpty(jwt))
            {
                var claims = new List<Claim>();
                var payload = jwt.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

                if (roles != null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

                return claims;
            }

            return null;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
