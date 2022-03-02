using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.ActivityLoggers;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;
using Serendip.IK.DamageCompensations;
using Serendip.IK.DamageCompensationsFileInfo;
using Serendip.IK.Extensions;
using Serendip.IK.KBolges;
using Serendip.IK.KInkaLookUpTables;
using Serendip.IK.KNormDetails;
using Serendip.IK.KNorms;
using Serendip.IK.KPersonels;
using Serendip.IK.KSubeNorms;
using Serendip.IK.KSubes;
using Serendip.IK.Mails;
using Serendip.IK.MultiTenancy;
using Serendip.IK.Nodes;
using Serendip.IK.Ops.DamageCurrent;
using Serendip.IK.Ops.Hierarchy;
using Serendip.IK.Ops.History;
using Serendip.IK.Ops.Interruption;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.Positions;
using Serendip.IK.Ops.Units;
using Serendip.IK.Periods;
using Serendip.IK.Positions;
using Serendip.IK.ProviderAccounts;
using Serendip.IK.TextTemplates;
using Serendip.IK.Transfers;
using Serendip.IK.Units;
using System.Reflection;

namespace Serendip.IK.EntityFrameworkCore
{
    public class IKDbContext : AbpZeroDbContext<Tenant, Role, User, IKDbContext>
    {
        public DbSet<KSube> KSube { get; set; }
        public DbSet<KHierarchy> KHierarchies { get; set; }
        public DbSet<KNorm> KNorm { get; set; }
        public DbSet<KNormDetail> KNormDetails { get; set; }
        public DbSet<KSubeNorm> KSubeNorm { get; set; }
        public DbSet<KBolge> KBolge { get; set; }
        public DbSet<KPersonel> KPersonel { get; set; }
        public DbSet<KInkaLookUpTable> KInkaLookUpTable { get; set; }
        public DbSet<Emails.Email> Emails { get; set; }
        public DbSet<Emails.EmailRecipient> EmailRecipients { get; set; }
        public DbSet<Emails.EmailAttachment> EmailAttachments { get; set; }
        public DbSet<Emails.EmailLink> EmailLinks { get; set; }
        public DbSet<ActivityLogger> ActivityLoggers { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Files.File> Files { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<TextTemplate> TextTemplates { get; set; }
        public DbSet<TransferHistory> TransferHistories { get; set; }
        public DbSet<ProviderAccount> ProviderAccounts { get; set; }
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<ExtensionItem> ExtensionItems { get; set; }
        public DbSet<MarketplaceItem> MarketplaceItems { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<DamageCompensation> DamageCompensations { get; set; }
        public DbSet<DamageCompensationFileInfo> DamageCompensationsFileInfos { get; set; }
        public DbSet<DamageCompensationEvaluation> DamageCompensationEvaluations { get; set; }

        public DbSet<OpsUnit> OpsUnits { get; set; }
        public DbSet<OpsNode> OpsNodes { get; set; }
        public DbSet<OpsPosition> OpsPositions { get; set; }
        public DbSet<OpsHierarchy> OpsHierarchy { get; set; }
        public DbSet<OpsHistroy> OpsHistroy { get; set; }

        public DbSet<Current> OpsCurrent { get; set; }

        public DbSet<OpsInterruption> OpsInterruption { get;set;}
   


        public IKDbContext(DbContextOptions<IKDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }
    }
}
