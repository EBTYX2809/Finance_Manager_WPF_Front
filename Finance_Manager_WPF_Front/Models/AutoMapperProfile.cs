using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;
using Microsoft.Extensions.DependencyInjection;

namespace Finance_Manager_WPF_Front.Models;

public class AutoMapperProfile : Profile
{   
    public AutoMapperProfile()
    {
        CreateMap<CurrencyBalanceDTO, UserCurrencyBalanceModel>();

        CreateMap<UserDTO, UserModel>();

        CreateMap<TransactionDTO, TransactionModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src =>
            TimeZoneInfo.ConvertTimeFromUtc(src.Date.DateTime, TimeZoneInfo.Local)))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src =>
            CategoriesStorage.GetCategoryById(src.CategoryId)))
            .ForMember(dest => dest.InnerCategory, opt => opt.MapFrom(src =>
            CategoriesStorage.GetCategoryById(src.InnerCategoryId ?? 0)));

        CreateMap<TransactionModel, TransactionDTO>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src =>
            TimeZoneInfo.ConvertTimeToUtc(src.Date.Date, TimeZoneInfo.Local)))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.InnerCategoryId, opt => opt.MapFrom(src => src.InnerCategory.Id));            

        CreateMap<SavingDTO, SavingModel>();
        CreateMap<SavingModel, SavingDTO>();            

        CreateMap<CategoryDTO, CategoryModel>();
        CreateMap<CategoryModel, CategoryDTO>(); // Мб добавить маппинг для ParentCategoryId        
    }
}
