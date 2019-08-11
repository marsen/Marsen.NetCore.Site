using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Marsen.NetCore.DA.Models
{
    public partial class MARSContext : DbContext
    {
        public MARSContext()
        {
        }

        public MARSContext(DbContextOptions<MARSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Member { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=MARS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.MemberId)
                    .HasName("IX_MemberAccount")
                    .IsUnique();

                entity.Property(e => e.MemberAccount)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
