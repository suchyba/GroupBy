using AutoMapper;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.Agreement;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.DTO.Position;
using GroupBy.Application.DTO.Rank;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountingBook, AccountingBookDTO>().ReverseMap();
            CreateMap<AccountingBook, AccountingBookCreateDTO>().ReverseMap();

            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Group, GroupCreateDTO>().ReverseMap();
            CreateMap<Group, GroupUpdateDTO>().ReverseMap();

            CreateMap<Volunteer, VolunteerDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerCreateDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerUpdateDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerSimpleDTO>();

            CreateMap<Agreement, AgreementDTO>().ReverseMap();
            CreateMap<Agreement, AgreementCreateDTO>().ReverseMap();

            CreateMap<Rank, RankDTO>();
            CreateMap<RankDTO, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));
            CreateMap<RankCreateDTO, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));

            CreateMap<Position, PositionDTO>();
            CreateMap<PositionDTO, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));
            CreateMap<PositionCreateDTO, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));

            CreateMap<InventoryItem, InventoryItemDTO>().ReverseMap();
            CreateMap<InventoryItem, InventoryItemCreateDTO>().ReverseMap();

            CreateMap<InventoryBook, InventoryBookDTO>();
            CreateMap<InventoryBookDTO, InventoryBook>();
            CreateMap<InventoryBookCreateDTO, InventoryBook>()
                .ForMember(dest => dest.RelatedGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.RelatedGroupId }));
            CreateMap<InventoryBookUpdateDTO, InventoryBook>()
                .ForMember(dest => dest.RelatedGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.RelatedGroupId }));
        }
    }
}
