using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GroupBy.Data.DbContexts
{
    public class GroupByDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        public DbSet<ApplicationUser> Identities { get; set; }
        public DbSet<InventoryItemTransfer> InventoryItemTransfers { get; set; }

        public GroupByDbContext(DbContextOptions<GroupByDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingBook>()
                .HasAlternateKey(a => new { a.BookIdentificator, a.BookOrderNumberId });
            modelBuilder.Entity<GroupsPermissions>()
                .HasAlternateKey(p => new { p.GroupId, p.PositionId });
            modelBuilder.Entity<PositionRecord>()
                .HasAlternateKey(p => new { p.VolunteerId, p.Id });

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Owner)
                .WithMany(v => v.OwnedGroups)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItemTransfer>()
                .HasOne(t => t.SourceInventoryBook)
                .WithMany(b => b.OutgoingInventoryItemTransfers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItemTransfer>()
                .HasOne(t => t.DestinationInventoryBook)
                .WithMany(b => b.IncomingInventoryItemTransfers)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
