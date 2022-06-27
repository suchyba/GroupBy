using FluentValidation;
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
using GroupBy.Application.DTO.Resolution;
using GroupBy.Application.Validators.Resolution;
using GroupBy.Application.Validators.FinancialOutcomeRecord;
using GroupBy.Application.DTO.FinancialOutcomeRecord;
using GroupBy.Application.Validators.FinancialIncomeRecord;
using GroupBy.Application.DTO.FinancialIncomeRecord;
using Microsoft.Extensions.Configuration;
using GroupBy.Application.DTO.RegistrationCode;
using GroupBy.Application.Validators.RegistrationCode;
using GroupBy.Application.Validators.Authentication;
using GroupBy.Application.DTO.Authentication;

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
            services.AddScoped<IResolutionService, ResolutionService>();
            services.AddScoped<IFinancialOutcomeRecordService, FinancialOutcomeRecordService>();
            services.AddScoped<IFinancialIncomeRecordService, FinancialIncomeRecordService>();
            services.AddScoped<IRegistrationCodeService, RegistrationCodeService>();
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<GroupDTO>, GroupValidator>();
            services.AddScoped<IValidator<GroupCreateDTO>, GroupCreateValidator>();
            services.AddScoped<IValidator<GroupUpdateDTO>, GroupUpdateValidator>();

            services.AddScoped<IValidator<AccountingBookSimpleDTO>, AccountingBookUpdateValidator>();
            services.AddScoped<IValidator<AccountingBookCreateDTO>, AccountingBookCreateValidator>();

            services.AddScoped<IValidator<VolunteerDTO>, VolunteerValidator>();
            services.AddScoped<IValidator<VolunteerCreateDTO>, VolunteerCreateValidator>();
            services.AddScoped<IValidator<VolunteerUpdateDTO>, VolunteerUpdateValidator>();

            services.AddScoped<IValidator<AgreementDTO>, AgreementValidator>();
            services.AddScoped<IValidator<AgreementCreateDTO>, AgreementCreateValidator>();

            services.AddScoped<IValidator<RankSimpleDTO>, RankValidator>();
            services.AddScoped<IValidator<RankCreateDTO>, RankCreateValidator>();

            services.AddScoped<IValidator<PositionSimpleDTO>, PositionValidator>();
            services.AddScoped<IValidator<PositionCreateDTO>, PositionCreateValidator>();

            services.AddScoped<IValidator<InventoryItemSimpleDTO>, InventoryItemValidator>();
            services.AddScoped<IValidator<InventoryItemCreateDTO>, InventoryItemCreateValidator>();

            services.AddScoped<IValidator<InventoryBookSimpleDTO>, InventoryBookValidator>();
            services.AddScoped<IValidator<InventoryBookCreateDTO>, InventoryBookCreateValidator>();
            services.AddScoped<IValidator<InventoryBookUpdateDTO>, InventoryBookUpdateValidator>();

            services.AddScoped<IValidator<DocumentDTO>, DocumentValidator>();
            services.AddScoped<IValidator<DocumentCreateDTO>, DocumentCreateValidator>();
            services.AddScoped<IValidator<DocumentUpdateDTO>, DocumentUpdateValidator>();

            services.AddScoped<IValidator<InventoryBookRecordSimpleDTO>, InventoryBookRecordValidator>();
            services.AddScoped<IValidator<InventoryBookRecordCreateDTO>, InventoryBookRecordCreateValidator>();
            services.AddScoped<IValidator<InventoryBookRecordUpdateDTO>, InventoryBookRecordUpdateValidator>();
            services.AddScoped<IValidator<InventoryBookRecordTransferDTO>, InventoryBookRecordTransferValidator>();

            services.AddScoped<IValidator<ProjectSimpleDTO>, ProjectValidator>();
            services.AddScoped<IValidator<ProjectCreateDTO>, ProjectCreateValidator>();
            services.AddScoped<IValidator<ProjectUpdateDTO>, ProjectUpdateValidator>();

            services.AddScoped<IValidator<InventoryItemSourceDTO>, InventoryItemSourceValidator>();
            services.AddScoped<IValidator<InventoryItemSourceCreateDTO>, InventoryItemSourceCreateValidator>();

            services.AddScoped<IValidator<AccountingDocumentSimpleDTO>, AccountingDocumentValidator>();
            services.AddScoped<IValidator<AccountingDocumentCreateDTO>, AccountingDocumentCreateValidator>();
            
            services.AddScoped<IValidator<ResolutionCreateDTO>, ResolutionCreateValidator>();
            services.AddScoped<IValidator<ResolutionUpdateDTO>, ResolutionUpdateValidator>();

            services.AddScoped<IValidator<FinancialOutcomeRecordCreateDTO>, FinancialOutcomeRecordCreateValidator>();
            services.AddScoped<IValidator<FinancialOutcomeRecordUpdateDTO>, FinancialOutcomeRecordUpdateValidator>();
            
            services.AddScoped<IValidator<FinancialIncomeRecordCreateDTO>, FinancialIncomeRecordCreateValidator>();
            services.AddScoped<IValidator<FinancialIncomeRecordUpdateDTO>, FinancialIncomeRecordUpdateValidator>();

            services.AddScoped<IValidator<RegistrationCodeCreateDTO>, RegistrationCodeCreateValidator>();
            services.AddScoped<IValidator<RegistrationCodeUpdateDTO>, RegistrationCodeUpdateValidator>();

            services.AddScoped<IValidator<RegisterDTO>, RegisterValidator>();
        }
    }
}
