using GroupBy.Application.Design;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GroupBy.Data.DbContexts
{
    public class GroupByDbContext : IdentityDbContext<IdentityModel>
    {
        public DbSet<FinancialRecord> FinancialRecords { get; set; }
        public DbSet<FinancialIncomeRecord> FinancialIncomeRecords { get; set; }
        public DbSet<FinancialOutcomeRecord> FinancialOutcomeRecords { get; set; }
        public DbSet<AccountingBook> AccountingBooks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<GroupsPermissions> Permissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Remainder> Remainders { get; set; }
        public DbSet<TODOList> TODOLists { get; set; }
        public DbSet<TODOListElement> TODOListElements { get; set; }
        public DbSet<AccountingDocument> AccountingDocuments { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionRecord> PositionRecords { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryBookRecord> InventoryBookRecords { get; set; }
        public DbSet<InventoryBook> InventoryBooks { get; set; }
        public DbSet<InvitationToGroup> InvitationToGroups { get; set; }
        public DbSet<InventoryItemSource> InventoryItemSources { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<RegistrationCode> RegistrationCodes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<IdentityModel> Identities { get; set; }
        /*public GroupByDbContext() : base()
        {

        }*/
        public GroupByDbContext(DbContextOptions<GroupByDbContext> options) : base(options)
        {

        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroupBy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingBook>()
                .HasKey(a => new { a.BookId, a.BookOrderNumberId });
            modelBuilder.Entity<GroupsPermissions>()
                .HasKey(p => new { p.GroupId, p.PositionId });
            modelBuilder.Entity<PositionRecord>()
                .HasKey(p => new { p.VolunteerId, p.Id });
            modelBuilder.Entity<InventoryBookRecord>()
                .HasKey(i => new { i.InventoryBookId, i.Id });
            modelBuilder.Entity<Resolution>()
                .HasKey(r => new { r.GroupId, r.Id });

            modelBuilder.Entity<Element>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Elements)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Owner)
                .WithMany(v => v.OwnedGroups)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
