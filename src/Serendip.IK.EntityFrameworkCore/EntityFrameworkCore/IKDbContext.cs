using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;
using Serendip.IK.KBolges;
using Serendip.IK.KInkaLookUpTables;
using Serendip.IK.KNormDetails;
using Serendip.IK.KNorms;
using Serendip.IK.KPersonels;
using Serendip.IK.KSubeNorms;
using Serendip.IK.KSubes;
using Serendip.IK.MultiTenancy;
using System.Reflection;


namespace Serendip.IK.EntityFrameworkCore
{
    public class IKDbContext : AbpZeroDbContext<Tenant, Role, User, IKDbContext>
    { 
        public DbSet<KSube> KSube { get; set; }
        public DbSet<KNorm> KNorm { get; set; }
        public DbSet<KNormDetail> KNormDetails { get; set; }
        public DbSet<KSubeNorm> KSubeNorm { get; set; }
        public DbSet<KBolge> KBolge { get; set; }
        public DbSet<KPersonel> KPersonel { get; set; }
        public DbSet<KInkaLookUpTable> KInkaLookUpTable { get; set; }  
        public IKDbContext(DbContextOptions<IKDbContext> options) : base(options) { } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
