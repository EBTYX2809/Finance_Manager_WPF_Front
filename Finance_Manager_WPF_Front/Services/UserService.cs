using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services;

public class UserService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private readonly IMapper _mapper;
    private UserSession _userSession;

    public UserService(FinanceApiClient apiClient, UserSession userSession, IMapper mapper, ApiWrapper apiWrapper)
    {
        _apiClient = apiClient;
        _userSession = userSession;
        _mapper = mapper;
        _apiWrapper = apiWrapper;
    }

    public async Task UpdateUserBalanceAsync()
    {
        var balanceDTO = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetBalanceAsync(_userSession.CurrentUser.Id));

        _userSession.CurrentUser.PrimaryCurrencyBalance = _mapper.Map<UserCurrencyBalanceModel>(balanceDTO.PrimaryBalance);
        // Possible null exception
        _userSession.CurrentUser.SecondaryCurrencyBalance1 = _mapper.Map<UserCurrencyBalanceModel>(balanceDTO.SecondaryBalance1);
        _userSession.CurrentUser.SecondaryCurrencyBalance2 = _mapper.Map<UserCurrencyBalanceModel>(balanceDTO.SecondaryBalance2);
    }
}
