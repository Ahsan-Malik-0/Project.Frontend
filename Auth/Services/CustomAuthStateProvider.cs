using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Project.Frontend.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Project.Frontend.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService localStorage;

        public CustomAuthStateProvider(HttpClient httpClient, ISyncLocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;

            var accessToken = localStorage.GetItem<string>("accessToken");
            if (accessToken != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public ISyncLocalStorageService LocalStorage => localStorage;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = localStorage.GetItem<string>("accessToken");
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : GetClaimsIdentity(token);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);

            //var user = new ClaimsPrincipal(new ClaimsIdentity()); // non-authenticate user
            //return new AuthenticationState(user);

        }

        public ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(token);
            var claims = JwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        public async Task<LoginResult> LoginAsync(LoginDto request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    localStorage.SetItem("accessToken", token);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // need to refresh auth state
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    return new LoginResult { Success = true };
                }

                var strRes = await response.Content.ReadAsStringAsync();
                var jsonRes = JsonNode.Parse(strRes);
                var error = jsonRes?["errors"]?.ToString() ?? string.Empty;

                var loginResult = new LoginResult
                {
                    Success = false,
                    Error = error
                };

                return loginResult;
                
            }
            catch
            {
                return new LoginResult { Success = false, Error = "An error occurred during login." };
            }
        }

        public void LogoutAsync()
        {
            localStorage.RemoveItem("accessToken");
            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public class LoginResult
        {
            public bool Success { get; set; }
            public string? Error { get; set; }
        }
    }
}
