using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Data.Entity;
using Microsoft.Extensions.Configuration;
using System.IO;
using DotNetCore.Utility;
namespace DotNetCore.Data.DbManager
{
    public partial class EntityCoreContext : DbContext
    {
        public EntityCoreContext(DbContextOptions<EntityCoreContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountRoleMap> AccountRoleMap { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryLayout> CategoryLayout { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<ContentCategoryMap> ContentCategoryMap { get; set; }
        public virtual DbSet<DefineRouting> DefineRouting { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }
        public virtual DbSet<Advertisement> Advertisement { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<ContentTagMap> ContentTagMap { get; set; }
        public virtual DbSet<Subscribe> Subscribe { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

        //        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentCategoryMap>()
                .HasKey(t => new { t.CategoryId, t.ContentId });

            modelBuilder.Entity<ContentTagMap>()
                .HasKey(t => new { t.TagId, t.ContentId });

            modelBuilder.Entity<AccountRoleMap>()
                .HasKey(t => new { t.AccountId, t.RoleId });
        }
    }
}
