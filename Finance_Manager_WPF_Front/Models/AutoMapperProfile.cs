using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;

namespace Finance_Manager_WPF_Front.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CurrencyBalanceDTO, UserCurrencyBalanceModel>();

        CreateMap<UserDTO, UserModel>();
    }
}
