using AutoMapper;
using GroupBy.Application.ViewModels;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountingBook, AccountingBookViewModel>().ReverseMap();
            CreateMap<Group, GroupViewModel>().ReverseMap();
            CreateMap<Volunteer, VolunteerViewModel>().ReverseMap();
        }
    }
}
