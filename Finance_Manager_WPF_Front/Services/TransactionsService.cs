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

    public async Task GetTransactionsPageAsync(int pageSize = 10)
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

    public async Task CreateTransactionAsync(TransactionModel transactionModel)
    {
        _userSession.CurrentUser.Transactions.Add(transactionModel);

        var transaction = _mapper.Map<TransactionDTO>(transactionModel);
        transaction.UserId = _userSession.CurrentUser.Id;

        int id = await _apiWrapper.ExecuteAsync(async () => 
        await _apiClient.CreateTransactionAsync(transaction));

        transactionModel.Id = id; // Recieved id from data base
    }

    public async Task UpdateTransactionAsync(TransactionModel transactionModel)
    {
        var oldTransaction = _userSession.CurrentUser.Transactions.FirstOrDefault(t => t.Id == transactionModel.Id);

        oldTransaction = transactionModel;

        var transaction = _mapper.Map<TransactionDTO>(transactionModel);
        transaction.UserId = _userSession.CurrentUser.Id;

        await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.UpdateTransactionAsync(transaction));
    }

    public async Task DeleteTransactionAsync(TransactionModel transactionModel)
    {
        _userSession.CurrentUser.Transactions.Remove(transactionModel);

        await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.DeleteTransactionAsync(transactionModel.Id));
    }
}
