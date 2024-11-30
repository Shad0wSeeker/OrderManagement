using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OrderManagement.Data.Entities;
using OrderManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace OrderManagement.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
        }

        // Проверка, авторизован ли пользователь
        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();
            return !string.IsNullOrEmpty(token);
        }

        // Получение имени пользователя из токена JWT
        public async Task<string> Username()
        {
            var token = await GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jsonToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
                return jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            }
            return null;
        }

        // Получение токена из localStorage
        private async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>("token");
        }

        // Логин (сохранение токена в localStorage)
        public async Task<LoginResponse> Login(string username, string password)
        {
            var loginRequest = new LoginRequest { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7113/api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                // Сохраняем токен в localStorage
                await _localStorage.SetItemAsync("token", loginResponse.Token);
                return loginResponse;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        // Логаут (удаление токена из localStorage)
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
        }

    }
}