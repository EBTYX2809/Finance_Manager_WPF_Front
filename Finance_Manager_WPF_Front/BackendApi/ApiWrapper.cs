using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Finance_Manager_WPF_Front.BackendApi;

public class ApiWrapper
{
    private readonly UserSession _userSession;    
    public ApiWrapper(UserSession userSession)
    {
        _userSession = userSession;
    }
    public async Task<T?> ExecuteAsync<T>(Func<Task<T>> apiCall)
    {
        try
        {
            return await apiCall();
        }
        catch (ApiException ex)
        {
            HandleApiException(ex);
            throw;
        }
        catch (Exception ex)
        {
            HandleUnexpectedException(ex);
            throw;
        }
    }

    private void HandleApiException(ApiException ex)
    {
        switch (ex.StatusCode)
        {
            case 400:
                MessageBox.Show("Validation failed.");
                break;
            case 401:
                MessageBox.Show("Authorization failed.");
                _userSession.Logout();
                break;
            case 403:
                MessageBox.Show("No access.");
                break;
            case 404:
                MessageBox.Show("Not found some resources.");
                break;
            case 500:
                MessageBox.Show("Server error.");
                break;
            default:
                MessageBox.Show($"API error: {ex.StatusCode}");
                break;
        }

        Serilog.Log.Error(ex, "Error in API.");
    }

    private void HandleUnexpectedException(Exception ex)
    {
        MessageBox.Show("Unknow error: " + ex.Message);
        Serilog.Log.Error(ex, "Unexpected Error in API.");
    }
}

