using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services;

public class AnalyticsService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private UserSession _userSession;

    public AnalyticsService(FinanceApiClient apiClient, ApiWrapper apiWrapper, UserSession userSession)
    {
        _apiClient = apiClient;
        _apiWrapper = apiWrapper;
        _userSession = userSession;
    }

    public async Task<List<CategoryPercentDTO>> GetAnalyticsForWeekAsync()
    {
        var analytics = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAnalyticsAsync(
            _userSession.CurrentUser.Id,
            new DateTimeOffset(DateTime.UtcNow.AddDays(-7)),
            new DateTimeOffset(DateTime.UtcNow)));

        return analytics.ToList();
    }

    public async Task<List<CategoryPercentDTO>> GetAnalyticsForMonthAsync()
    {
        var analytics = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAnalyticsAsync(
            _userSession.CurrentUser.Id,
            new DateTimeOffset(DateTime.UtcNow.AddMonths(-1)),
            new DateTimeOffset(DateTime.UtcNow)));

        return analytics.ToList();
    }

    public async Task<List<CategoryPercentDTO>> GetAnalyticsFor3MonthAsync()
    {
        var analytics = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAnalyticsAsync(
            _userSession.CurrentUser.Id,
            new DateTimeOffset(DateTime.UtcNow.AddMonths(-3)),
            new DateTimeOffset(DateTime.UtcNow)));

        return analytics.ToList();
    }

    public async Task<List<CategoryPercentDTO>> GetAnalyticsForHalfYearAsync()
    {
        var analytics = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAnalyticsAsync(
            _userSession.CurrentUser.Id,
            new DateTimeOffset(DateTime.UtcNow.AddMonths(-6)),
            new DateTimeOffset(DateTime.UtcNow)));

        return analytics.ToList();
    }

    public async Task<List<CategoryPercentDTO>> GetAnalyticsForYearAsync()
    {
        var analytics = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAnalyticsAsync(
            _userSession.CurrentUser.Id,
            new DateTimeOffset(DateTime.UtcNow.AddYears(-1)),
            new DateTimeOffset(DateTime.UtcNow)));

        return analytics.ToList();
    }
}