using System.Text;
using System.Text.Json;

namespace Finance_Manager_WPF_Front_Tests;

public class TokensTests
{
    [Fact]
    public void IsAccessTokenActual_ValidToken_ReturnsTrue()
    {
        // Arrange
        var token = CreateJwtToken(expOffsetSeconds: 60);

        // Act
        var result = IsAccessTokenActual(token);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAccessTokenActual_ExpiredToken_ReturnsFalse()
    {
        // Arrange
        var token = CreateJwtToken(expOffsetSeconds: -60);

        // Act
        var result = IsAccessTokenActual(token);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsAccessTokenActual_ExpiresNow_ReturnsFalse()
    {
        // Arrange: срок действия — прямо сейчас
        var token = CreateJwtToken(expOffsetSeconds: 0);

        // Act
        var result = IsAccessTokenActual(token);

        // Assert
        Assert.False(result); // Можно Assert.True, если хочешь разрешать равенство
    }

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

    private string CreateJwtToken(int expOffsetSeconds)
    {
        var exp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + expOffsetSeconds;
        var payloadJson = $"{{\"exp\":{exp}}}";
        var payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
        var payload = Convert.ToBase64String(payloadBytes)
                            .TrimEnd('=')
                            .Replace('+', '-')
                            .Replace('/', '_'); // Base64Url

        return $"header.{payload}.signature";
    }
}