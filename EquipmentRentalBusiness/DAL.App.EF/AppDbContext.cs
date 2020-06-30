using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Domain.App;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly IUserNameProvider _userNameProvider;

        public DbSet<AppUserCompany> AppUserCompanies { get; set; } = default!;
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Image> Images { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemBooked> ItemsBooked { get; set; } = default!;
        public DbSet<ItemCategory> ItemCategories { get; set; } = default!;
        public DbSet<ItemDescription> ItemDescriptions { get; set; } = default!;
        public DbSet<ItemOwnership> ItemOwnerships { get; set; } = default!;
        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<ProductDescription> ProductDescriptions { get; set; } = default!;
        public DbSet<RentalPeriod> RentalPeriods { get; set; } = default!;
        
        public DbSet<LangStr> LangStrs { get; set; } = default!;
        public DbSet<LangStrTranslation> LangStrTranslation { get; set; } = default!;

        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // enable cascade delete on Item -> ItemCategories
            builder.Entity<Item>()
                .HasMany(c => c.ItemCategories)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> Images
            builder.Entity<Item>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> ItemOwnerships
            builder.Entity<Item>()
                .HasMany(c => c.ItemOwnerships)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> Bookings
            builder.Entity<Item>()
                .HasMany(c => c.Bookings)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> ItemsBooked
            builder.Entity<Item>()
                .HasMany(c => c.ItemsBooked)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> Prices
            builder.Entity<Item>()
                .HasMany(c => c.Prices)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> ItemDescriptions
            builder.Entity<Item>()
                .HasMany(c => c.ItemDescriptions)
                .WithOne(i => i.Item!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Item -> Location
            builder.Entity<Item>()
                .HasOne(c => c.Location)
                .WithMany(i => i!.Items)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Booking -> ItemBooked
            builder.Entity<Booking>()
                .HasMany(i => i.ItemsBooked)
                .WithOne(i => i.Booking!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Invoice -> Bookings
            builder.Entity<Invoice>()
                .HasMany(i => i.Bookings)
                .WithOne(i => i.Invoice!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on Invoice -> Payments
            builder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(i => i.Invoice!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // enable cascade delete on LangStr->LangStrTranslations
            builder.Entity<LangStr>()
                .HasMany(s => s.Translations)
                .WithOne(l => l.LangStr!)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LangStrTranslation>().HasIndex(i => new {i.Culture, i.LangStrId}).IsUnique();
        }

        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.DeletedAt = DateTime.MaxValue;
                entityWithMetaData.DeletedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.DeletedAt = DateTime.Now;
                entityWithMetaData.DeletedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}