using AutoMapper;
using GroupBy.Application.ViewModels.AccountingBook;
using GroupBy.Application.ViewModels.Agreement;
using GroupBy.Application.ViewModels.Group;
using GroupBy.Application.ViewModels.Position;
using GroupBy.Application.ViewModels.Rank;
using GroupBy.Application.ViewModels.Volunteer;
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
            CreateMap<Volunteer, VolunteerSimpleViewModel>();

            CreateMap<Agreement, AgreementViewModel>().ReverseMap();
            CreateMap<Agreement, AgreementCreateViewModel>().ReverseMap();

            CreateMap<Rank, RankViewModel>();
            CreateMap<RankViewModel, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));
            CreateMap<RankCreateViewModel, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));

            CreateMap<Position, PositionViewModel>();
            CreateMap<PositionViewModel, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));
            CreateMap<PositionCreateViewModel, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));
        }
    }
}
