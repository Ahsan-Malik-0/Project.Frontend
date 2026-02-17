using Microsoft.AspNetCore.Components.Authorization;
using Project.Frontend.Model;
using System.Data;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Xml.Linq;

namespace Project.Frontend.Services
{
    public class SharedServices
    {
        private readonly AuthenticationStateProvider _provider;
        public HttpClient httpClient;

        public SharedServices(AuthenticationStateProvider provider, HttpClient httpClient)
        {
            _provider = provider;
            this.httpClient = httpClient;
        }


        public async Task<Claims> GetClaims()
        {
            var authStateProvider = (CustomAuthStateProvider)_provider;
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var name = user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
            var role = user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            return new Claims { Id = id, UserName = name, Role = role };
        }

        public async Task<Guid?> GetSocietyIdAsync(string memberId)
        {
            var response = await httpClient.GetAsync(
                $"president/getSocietyId?memberId={memberId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Guid>();
        }
    }

}
