using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Finance_Manager_WPF_Front.Services.AuthServices;
using System.Windows;

namespace Finance_Manager_WPF_Front.BackendApi;

public partial class FinanceApiClient
{
    private readonly TokensManager _tokensManager;
    public Func<Task> UpdateUserBalanceAfterBackendResponse;

    public FinanceApiClient(string baseUrl, HttpClient httpClient, TokensManager tokensManager)
        : this(baseUrl, httpClient)
    {
        _tokensManager = tokensManager;
    }

    // May be need override        
    private async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, 
        string url, CancellationToken cancellationToken = default)
    {
        if (url.Contains("api/Auth")) return;

        var token = await _tokensManager.GetAccessTokenAsync();        

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, 
        StringBuilder urlBuilder, CancellationToken cancellationToken = default)
    {
        if (urlBuilder.ToString().Contains("api/Auth")) return;

        var token = await _tokensManager.GetAccessTokenAsync();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if(response.RequestMessage.Method.ToString() != "GET" 
            && !response.RequestMessage.RequestUri.AbsolutePath.Contains("api/Auth"))
        {
            await UpdateUserBalanceAfterBackendResponse.Invoke(); // Subscribed in UserService
        }
    }
}
