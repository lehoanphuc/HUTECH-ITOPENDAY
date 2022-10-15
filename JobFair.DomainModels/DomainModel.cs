using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace JobFair.DomainModels
{
    public partial class DomainModel : DbContext
    {
        public DomainModel()
            : base("name=DomainModel")
        {
        }

        public virtual DbSet<COMPANY> COMPANies { get; set; }
        public virtual DbSet<COMPANY_JOB> COMPANY_JOB { get; set; }
        public virtual DbSet<EVENT> EVENTs { get; set; }
        public virtual DbSet<JOBTITLE> JOBTITLEs { get; set; }
        public virtual DbSet<ROLE> ROLEs { get; set; }
        public virtual DbSet<SETTING> SETTINGs { get; set; }
        public virtual DbSet<STUDENT_EVENT> STUDENT_EVENT { get; set; }
        public virtual DbSet<STUDENT_JOB> STUDENT_JOB { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<SCHOLARSHIP_STUDENT> SCHOLARSHIP_STUDENT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<COMPANY>()
                .Property(e => e.CompanyVideo)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .HasMany(e => e.COMPANY_JOB)
                .WithRequired(e => e.COMPANY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COMPANY_JOB>()
                .HasMany(e => e.STUDENT_JOB)
                .WithRequired(e => e.COMPANY_JOB)
                .HasForeignKey(e => new { e.IdCompany, e.IdTitle })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EVENT>()
                .HasMany(e => e.STUDENT_EVENT)
                .WithRequired(e => e.EVENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JOBTITLE>()
                .HasMany(e => e.COMPANY_JOB)
                .WithRequired(e => e.JOBTITLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERs)
                .WithRequired(e => e.ROLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SETTING>()
                .Property(e => e.SettingKey)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.STUDENT_JOB)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);
        }
    }
}
