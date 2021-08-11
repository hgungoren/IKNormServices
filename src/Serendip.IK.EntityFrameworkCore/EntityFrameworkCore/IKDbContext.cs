﻿using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.ActivityLoggers;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Extensions;
using Serendip.IK.KBolges;
using Serendip.IK.KHierarchies;
using Serendip.IK.KInkaLookUpTables;
using Serendip.IK.KNormDetails;
using Serendip.IK.KNorms;
using Serendip.IK.KPersonels;
using Serendip.IK.KSubeNorms;
using Serendip.IK.KSubes;
using Serendip.IK.Mails;
using Serendip.IK.MultiTenancy;
using Serendip.IK.Periods;
using Serendip.IK.ProviderAccounts;
using Serendip.IK.TextTemplates;
using Serendip.IK.Transfers;
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

        public IKDbContext(DbContextOptions<IKDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
