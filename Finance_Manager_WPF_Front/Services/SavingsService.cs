using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services;

public class SavingsService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private readonly IMapper _mapper;
    private UserSession _userSession;

    public SavingsService(FinanceApiClient apiClient, ApiWrapper apiWrapper, IMapper mapper, UserSession userSession)
    {
        _apiClient = apiClient;
        _apiWrapper = apiWrapper;
        _mapper = mapper;
        _userSession = userSession;
    }

    public async Task GetSavingsPage(int pageSize = 5)
    {
        int lastId = 1; // Должно быть 0, но тут костыль с GetSavingsAsync в котором previousSavingId это int? и оно 0 приводит к null.

        if (_userSession.CurrentUser.Savings.Count > 0)
        {
            lastId = _userSession.CurrentUser.Savings.Last().Id;
        }

        var savingsDTOs = await _apiWrapper.ExecuteAsync(async () => 
        await _apiClient.GetSavingsAsync(
                userId: _userSession.CurrentUser.Id,
                previousSavingId: lastId,
                pageSize: pageSize));

        var savings = _mapper.Map<List<SavingModel>>(savingsDTOs);

        foreach (var saving in savings)
        {
            _userSession.CurrentUser.Savings.Add(saving);
        }
    }
}
