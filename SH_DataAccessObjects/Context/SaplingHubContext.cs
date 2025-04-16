using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SH_BusinessObjects.Common;
using SH_BusinessObjects.Common.Interface;
using SH_DataAccessObjects.Context.Interceptor;
using SH_BusinessObjects.Entities;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.Context
{
    public class SaplingHubContext(
        DbContextOptions<SaplingHubContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : ApiAuthorizationDbContext<ApplicationUser>(options, operationalStoreOptions), IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        public DbSet<Sapling> Saplings => Set<Sapling>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<AuctionBid> AuctionBids => Set<AuctionBid>();
        public DbSet<AuctionPlant> AuctionPlants => Set<AuctionPlant>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<WalletTransaction> WalletTransactions => Set<WalletTransaction>();

        // seed data function

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public DbSet<T> Get<T>() where T : BaseAuditableEntity => Set<T>();
        public DbSet<T> GetUser<T>() where T : IdentityUser => Set<T>();
    }
}
