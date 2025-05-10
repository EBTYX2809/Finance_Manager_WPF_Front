using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Finance_Manager_WPF_Front.Services.AuthServices;

public class TokensManager
{
    private readonly IServiceProvider _provider; // Need for break cycle references with FinanceApiClient
    private readonly UserSession _userSession;
    private string _filePath;

    public TokensManager(IServiceProvider provider, UserSession userSession)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appData, "My Finance Manager");
        Directory.CreateDirectory(dir);
        _filePath = Path.Combine(dir, "refresh_token.dat");
        _provider = provider;
        _userSession = userSession;
        _userSession.LogoutAction += DeleteRefreshToken;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var token = _userSession.AccessToken;

        if (token != null && IsAccessTokenActual(token))
        {
            return token;
        }
        else
        {
            var refresh = LoadRefreshToken();
            if (refresh == null) return null;

            var _apiClient = _provider.GetRequiredService<FinanceApiClient>();

            var response = await _apiClient.RefreshTokenAsync(refresh);

            SaveRefreshToken(response.RefreshToken);
            _userSession.AccessToken = response.AccessJwtToken;

            return response.AccessJwtToken;
        }
    }

    // Copied to tests
    private bool IsAccessTokenActual(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken)) return false;

        try
        {
            var parts = accessToken.Split('.');
            if (parts.Length != 3)
                return false;

            var payload = parts[1];
            // JWT payload is Base64Url encoded
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

            var exp = JsonDocument.Parse(json).RootElement.GetProperty("exp").GetInt64();

            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp);
            return expirationTime > DateTimeOffset.UtcNow;
        }
        catch
        {
            return false;
        }
    }

    public void SaveRefreshToken(string refreshToken)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
        var encryptedBytes = ProtectedData.Protect(tokenBytes, null, DataProtectionScope.CurrentUser);

        File.WriteAllBytes(_filePath, encryptedBytes);
    }

    public string? LoadRefreshToken()
    {
        try
        {
            var encryptedBytes = File.ReadAllBytes(_filePath);
            var decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        catch
        {
            return null;
        }
    }

    public void DeleteRefreshToken()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
}
