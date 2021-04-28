using AutoMapper;
using GroupBy.Application.ViewModels;
using GroupBy.Domain;
using System.Text.RegularExpressions;

namespace GroupBy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountingBook, AccountingBookViewModel>().ReverseMap();
        }
    }
}
