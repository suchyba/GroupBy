﻿using FluentValidation;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Services;
using GroupBy.Application.Validators.AccountingBook;
using GroupBy.Application.Validators.Agreement;
using GroupBy.Application.Validators.Group;
using GroupBy.Application.Validators.InventoryItem;
using GroupBy.Application.Validators.Position;
using GroupBy.Application.Validators.Rank;
using GroupBy.Application.Validators.Volunteer;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.Agreement;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.DTO.Position;
using GroupBy.Application.DTO.Rank;
using GroupBy.Application.DTO.Volunteer;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.Validators.InventoryBook;
using GroupBy.Application.Validators.Document;
using GroupBy.Application.DTO.Document;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.Validators.InventoryBookRecord;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.Validators.Project;
using GroupBy.Application.Validators.InventoryItemSource;
using GroupBy.Application.DTO.InventoryItemSource;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.Validators.AccountingDocument;

namespace GroupBy.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            RegisterValidators(services);

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAccountingBookService, AccountingBookService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IInventoryItemService, InventoryItemService>();
            services.AddScoped<IInventoryBookService, InventoryBookService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IInventoryBookRecordService, InventoryBookRecordService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IInventoryItemSourceService, InventoryItemSourceService>();
            services.AddScoped<IAccountingDocumentService, AccountingDocumentService>();

            return services;
        }
        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<GroupDTO>, GroupValidator>();
            services.AddScoped<IValidator<GroupCreateDTO>, GroupCreateValidator>();
            services.AddScoped<IValidator<GroupUpdateDTO>, GroupUpdateValidator>();

            services.AddScoped<IValidator<AccountingBookDTO>, AccountingBookUpdateValidator>();
            services.AddScoped<IValidator<AccountingBookCreateDTO>, AccountingBookCreateValidator>();

            services.AddScoped<IValidator<VolunteerDTO>, VolunteerValidator>();
            services.AddScoped<IValidator<VolunteerCreateDTO>, VolunteerCreateValidator>();
            services.AddScoped<IValidator<VolunteerUpdateDTO>, VolunteerUpdateValidator>();

            services.AddScoped<IValidator<AgreementDTO>, AgreementValidator>();
            services.AddScoped<IValidator<AgreementCreateDTO>, AgreementCreateValidator>();

            services.AddScoped<IValidator<RankDTO>, RankValidator>();
            services.AddScoped<IValidator<RankCreateDTO>, RankCreateValidator>();

            services.AddScoped<IValidator<PositionDTO>, PositionValidator>();
            services.AddScoped<IValidator<PositionCreateDTO>, PositionCreateValidator>();

            services.AddScoped<IValidator<InventoryItemDTO>, InventoryItemValidator>();
            services.AddScoped<IValidator<InventoryItemCreateDTO>, InventoryItemCreateValidator>();

            services.AddScoped<IValidator<InventoryBookDTO>, InventoryBookValidator>();
            services.AddScoped<IValidator<InventoryBookCreateDTO>, InventoryBookCreateValidator>();
            services.AddScoped<IValidator<InventoryBookUpdateDTO>, InventoryBookUpdateValidator>();

            services.AddScoped<IValidator<DocumentDTO>, DocumentValidator>();
            services.AddScoped<IValidator<DocumentCreateDTO>, DocumentCreateValidator>();
            services.AddScoped<IValidator<DocumentUpdateDTO>, DocumentUpdateValidator>();

            services.AddScoped<IValidator<InventoryBookRecordDTO>, InventoryBookRecordValidator>();
            services.AddScoped<IValidator<InventoryBookRecordCreateDTO>, InventoryBookRecordCreateValidator>();
            services.AddScoped<IValidator<InventoryBookRecordUpdateDTO>, InventoryBookRecordUpdateValidator>();

            services.AddScoped<IValidator<ProjectDTO>, ProjectValidator>();
            services.AddScoped<IValidator<ProjectCreateDTO>, ProjectCreateValidator>();
            services.AddScoped<IValidator<ProjectUpdateDTO>, ProjectUpdateValidator>();

            services.AddScoped<IValidator<InventoryItemSourceDTO>, InventoryItemSourceValidator>();
            services.AddScoped<IValidator<InventoryItemSourceCreateDTO>, InventoryItemSourceCreateValidator>();

            services.AddScoped<IValidator<AccountingDocumentDTO>, AccountingDocumentValidator>();
            services.AddScoped<IValidator<AccountingDocumentCreateDTO>, AccountingDocumentCreateValidator>();
        }
    }
}
