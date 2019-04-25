﻿// <auto-generated />
using Marsen.NetCore.DA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marsen.NetCore.DA.Migrations
{
    [DbContext(typeof(MARSContext))]
    [Migration("20190407114347_migration_create_member_table")]
    partial class migration_create_member_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Marsen.NetCore.DA.Models.Member", b =>
                {
                    b.Property<long>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MemberAccount")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("MemberName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("MemberId");

                    b.HasIndex("MemberId")
                        .IsUnique()
                        .HasName("IX_MemberAccount");

                    b.ToTable("Member");
                });
#pragma warning restore 612, 618
        }
    }
}