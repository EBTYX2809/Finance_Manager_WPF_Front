using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services;

public class CategoriesService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private readonly IMapper _mapper;

    public CategoriesService(FinanceApiClient apiClient, ApiWrapper apiWrapper, IMapper mapper)
    {
        _apiClient = apiClient;
        _apiWrapper = apiWrapper;
        _mapper = mapper;
    }

    public async Task LoadAllCategoriesAsync()
    {
        var categoriesDTOs = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.GetAllCategoriesAsync());

        var categories = _mapper.Map<List<CategoryModel>>(categoriesDTOs);

        CategoriesStorage.AllCategories = categories;
    }
}
