using AutoMapper;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.DTO.Agreement;
using GroupBy.Application.DTO.Authentication;
using GroupBy.Application.DTO.Document;
using GroupBy.Application.DTO.FinancialIncomeRecord;
using GroupBy.Application.DTO.FinancialOutcomeRecord;
using GroupBy.Application.DTO.FinancialRecord;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.DTO.InventoryItemSource;
using GroupBy.Application.DTO.Position;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.DTO.Rank;
using GroupBy.Application.DTO.RegistrationCode;
using GroupBy.Application.DTO.Resolution;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Domain.Entities;
using System;
using System.Linq;

namespace GroupBy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountingBook, AccountingBookSimpleDTO>().ReverseMap();
            CreateMap<AccountingBook, AccountingBookDTO>();
            CreateMap<AccountingBook, AccountingBookCreateDTO>().ReverseMap();

            CreateMap<Group, GroupDTO>();
            CreateMap<Group, GroupSimpleDTO>()
                .ForMember(dest => dest.HasInventoryBook, opt => opt.MapFrom(
                    src => src.InventoryBook != null));
            CreateMap<GroupSimpleDTO, Group>();

            CreateMap<Group, GroupCreateDTO>().ReverseMap();
            CreateMap<Group, GroupUpdateDTO>().ReverseMap();

            CreateMap<Volunteer, VolunteerDTO>().ReverseMap();
            CreateMap<VolunteerCreateDTO, Volunteer>()
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(
                    src => src.RankId.HasValue ? new Rank { Id = src.RankId.Value } : null));
            CreateMap<Volunteer, VolunteerUpdateDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerSimpleDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(
                    src => src.Identity == null ? null : src.Identity.Email))
                .ReverseMap();

            CreateMap<Agreement, AgreementDTO>().ReverseMap();
            CreateMap<Agreement, AgreementCreateDTO>().ReverseMap();

            CreateMap<Rank, RankSimpleDTO>();
            CreateMap<RankSimpleDTO, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));
            CreateMap<Rank, RankDTO>();
            CreateMap<RankCreateDTO, Rank>()
                .ForMember(dest => dest.HigherRank, opt => opt.MapFrom(
                    src => src.HigherRankId.HasValue ? new Rank { Id = src.HigherRankId.Value } : null));

            CreateMap<Position, PositionSimpleDTO>();
            CreateMap<Position, PositionDTO>();
            CreateMap<PositionSimpleDTO, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));
            CreateMap<PositionCreateDTO, Position>()
                .ForMember(dest => dest.HigherPosition, opt => opt.MapFrom(
                    src => src.HigherPositionId == null ? null : new Position { Id = src.HigherPositionId.Value }));

            CreateMap<InventoryItem, InventoryItemSimpleDTO>().ReverseMap();
            CreateMap<InventoryItem, InventoryItemCreateDTO>().ReverseMap();

            CreateMap<InventoryBook, InventoryBookSimpleDTO>();
            CreateMap<InventoryBookSimpleDTO, InventoryBook>();
            CreateMap<InventoryBook, InventoryBookDTO>();
            CreateMap<InventoryBookCreateDTO, InventoryBook>()
                .ForMember(dest => dest.RelatedGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.RelatedGroupId }));
            CreateMap<InventoryBookUpdateDTO, InventoryBook>()
                .ForMember(dest => dest.RelatedGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.RelatedGroupId }));

            CreateMap<Document, DocumentSimpleDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<DocumentCreateDTO, Document>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(
                    src => src.GroupsId.Select(gId => new Group { Id = gId })))
                .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(
                    src => src.RelatedProjectId.HasValue ? new Project { Id = src.RelatedProjectId.Value } : null));
            CreateMap<DocumentUpdateDTO, Document>()
                 .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(
                    src => src.RelatedProjectId.HasValue ? new Project { Id = src.RelatedProjectId.Value } : null));

            CreateMap<InventoryBookRecord, InventoryBookRecordSimpleDTO>();
            CreateMap<InventoryBookRecord, InventoryBookRecordDTO>()
                .ForMember(dest => dest.InventoryBook, opt => opt.MapFrom(
                    src => src.Book));
            CreateMap<InventoryBookRecord, InventoryBookRecordListDTO>();
            CreateMap<InventoryBookRecordSimpleDTO, InventoryBookRecord>();
            CreateMap<InventoryBookRecordCreateDTO, InventoryBookRecord>()
                .ForMember(dest => dest.Source, opt => opt.MapFrom(
                    src => new InventoryItemSource { Id = src.SourceId }))
                .ForMember(dest => dest.Item, opt => opt.MapFrom(
                    src => new InventoryItem { Id = src.ItemId }))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(
                    src => new Document { Id = src.DocumentId }));
            CreateMap<InventoryBookRecordUpdateDTO, InventoryBookRecord>()
                .ForMember(dest => dest.Source, opt => opt.MapFrom(
                    src => new InventoryItemSource { Id = src.SourceId }))
                .ForMember(dest => dest.Item, opt => opt.MapFrom(
                    src => new InventoryItem { Id = src.ItemId }))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(
                    src => new Document { Id = src.DocumentId }));

            CreateMap<ProjectSimpleDTO, Project>().ReverseMap();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.ParentGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.ParentGroupId }))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(
                    src => new Volunteer { Id = src.OwnerId }));
            CreateMap<ProjectUpdateDTO, Project>()
                .ForMember(dest => dest.ParentGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.ParentGroupId }))
                .ForMember(dest => dest.ProjectGroup, opt => opt.MapFrom(
                    src => src.ProjectGroupId.HasValue ? new Group { Id = src.ProjectGroupId.Value } : null))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(
                    src => new Volunteer { Id = src.OwnerId }));

            CreateMap<InventoryItemSource, InventoryItemSourceDTO>().ReverseMap();
            CreateMap<InventoryItemSource, InventoryItemSourceCreateDTO>().ReverseMap();

            CreateMap<AccountingDocument, AccountingDocumentSimpleDTO>().ReverseMap();
            CreateMap<AccountingDocument, AccountingDocumentDTO>();
            CreateMap<AccountingDocumentCreateDTO, AccountingDocument>()
                .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(
                    src => src.RelatedProjectId.HasValue ? new Project { Id = src.RelatedProjectId.Value } : null))
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(
                    src => src.GroupsId.Select(gId => new Group { Id = gId })));

            CreateMap<Resolution, ResolutionDTO>().ReverseMap();
            CreateMap<ResolutionCreateDTO, Resolution>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(
                    src => new Group { Id = src.GroupId }))
                .ForMember(dest => dest.Legislator, opt => opt.MapFrom(
                    src => new Volunteer { Id = src.LegislatorId }));
            CreateMap<ResolutionUpdateDTO, Resolution>();

            CreateMap<FinancialIncomeRecord, FinancialRecordSimpleDTO>()
                .ForMember(dest => dest.OtherIncome, opt => opt.MapFrom(
                    src => src.Other));
            CreateMap<FinancialOutcomeRecord, FinancialRecordSimpleDTO>()
                .ForMember(dest => dest.OtherOutcome, opt => opt.MapFrom(
                    src => src.Other));

            CreateMap<FinancialIncomeRecord, FinancialRecordDTO>()
                .ForMember(dest => dest.OtherIncome, opt => opt.MapFrom(
                    src => src.Other));
            CreateMap<FinancialOutcomeRecord, FinancialRecordDTO>()
                .ForMember(dest => dest.OtherOutcome, opt => opt.MapFrom(
                    src => src.Other));

            CreateMap<FinancialOutcomeRecord, FinancialOutcomeRecordSimpleDTO>().ReverseMap();
            CreateMap<FinancialOutcomeRecord, FinancialOutcomeRecordDTO>();
            CreateMap<FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecord>()
                .ForMember(dest => dest.RelatedDocument, opt => opt.MapFrom(
                    src => new AccountingDocument { Id = src.RelatedDocumentId }))
                .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(
                    src => src.RelatedProjectId.HasValue ? new Project { Id = src.RelatedProjectId.Value } : null));
            CreateMap<FinancialOutcomeRecordUpdateDTO, FinancialOutcomeRecord>();

            CreateMap<FinancialIncomeRecord, FinancialIncomeRecordSimpleDTO>().ReverseMap();
            CreateMap<FinancialIncomeRecord, FinancialIncomeRecordDTO>();
            CreateMap<FinancialIncomeRecordCreateDTO, FinancialIncomeRecord>()
                .ForMember(dest => dest.RelatedDocument, opt => opt.MapFrom(
                    src => new AccountingDocument { Id = src.RelatedDocumentId }))
                .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(
                    src => src.RelatedProjectId.HasValue ? new Project { Id = src.RelatedProjectId.Value } : null));
            CreateMap<FinancialIncomeRecordUpdateDTO, FinancialIncomeRecord>();

            CreateMap<RegistrationCode, RegistrationCodeSimpleDTO>().ReverseMap();
            CreateMap<RegistrationCode, RegistrationCodeFullDTO>();
            CreateMap<RegistrationCodeCreateDTO, RegistrationCode>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(
                    src => new Volunteer { Id = src.OwnerId }))
                .ForMember(dest => dest.TargetGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.TargetGroupId }))
                .ForMember(dest => dest.TargetRank, opt => opt.MapFrom(
                    src => src.TargetRankId.HasValue ? new Rank { Id = src.TargetRankId.Value } : null));
            CreateMap<RegistrationCodeUpdateDTO, RegistrationCode>()
                .ForMember(dest => dest.TargetGroup, opt => opt.MapFrom(
                    src => new Group { Id = src.TargetGroupId }))
                .ForMember(dest => dest.TargetRank, opt => opt.MapFrom(
                    src => src.TargetRankId.HasValue ? new Rank { Id = src.TargetRankId.Value } : null));

            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(dest => dest.RelatedVolunteer, opt => opt.MapFrom(
                    src => new Volunteer
                    {
                        FirstNames = src.RelatedVolunteerFirstNames,
                        LastName = src.RelatedVolunteerLastName,
                        Address = src.RelatedVolunteerAddress,
                        BirthDate = src.RelatedVolunteerBirthDate,
                        PhoneNumber = src.RelatedVolunteerPhoneNumber
                    }));
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
