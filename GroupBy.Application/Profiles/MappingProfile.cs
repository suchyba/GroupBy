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
            CreateMap<AccountingBook, AccountingBookCreateViewModel>().ReverseMap();

            CreateMap<Group, GroupViewModel>().ReverseMap();
            CreateMap<Group, GroupCreateViewModel>().ReverseMap();
            CreateMap<Group, GroupUpdateViewModel>().ReverseMap();

            CreateMap<Volunteer, VolunteerViewModel>().ReverseMap();
            CreateMap<Volunteer, VolunteerCreateViewModel>().ReverseMap();
            CreateMap<Volunteer, VolunteerUpdateViewModel>().ReverseMap();
            CreateMap<Volunteer, SimpleVolunteerViewModel>();
        }
    }
}
