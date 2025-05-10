using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services;

public class TransactionsService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private readonly IMapper _mapper;
    private UserSession _userSession;

    public TransactionsService(FinanceApiClient apiClient, ApiWrapper apiWrapper, IMapper mapper, UserSession userSession)
    {
        _apiClient = apiClient;
        _apiWrapper = apiWrapper;
        _mapper = mapper;
        _userSession = userSession;
    }

    public async Task GetTransactionsPage(int pageSize = 10)
    {
        DateTimeOffset date = new DateTimeOffset(DateTime.Now.ToUniversalTime(), TimeSpan.Zero);

        if (_userSession.CurrentUser.Transactions.Count > 0)
        {
            date = new DateTimeOffset(_userSession.CurrentUser.Transactions.Last().Date.ToUniversalTime(), // Convert to UTC
                TimeSpan.Zero);
        }

        var transactionsDTOs = await _apiWrapper.ExecuteAsync(async () => 
        await _apiClient.GetTransactionsAsync(
            userId: _userSession.CurrentUser.Id,
            lastDate: date,
            pageSize: pageSize));

        var transactions = _mapper.Map<List<TransactionModel>>(transactionsDTOs);

        foreach (var transaction in transactions)
        {
            _userSession.CurrentUser.Transactions.Add(transaction);
        }
    }
}
