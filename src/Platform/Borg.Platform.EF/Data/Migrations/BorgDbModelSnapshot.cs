﻿// <auto-generated />
using System;
using Borg.Platform.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Borg.Platform.EF.Data.Migrations
{
    [DbContext(typeof(BorgDb))]
    partial class BorgDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.CmsMenu_Id_seq", "'CmsMenu_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsMenuItem_Id_seq", "'CmsMenuItem_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsPage_Id_seq", "'CmsPage_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsRole_Id_seq", "'CmsRole_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsRolePermission_Id_seq", "'CmsRolePermission_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsUser_Id_seq", "'CmsUser_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.CmsUserPermission_Id_seq", "'CmsUserPermission_Id_seq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CultureName");

                    b.Property<string>("Title");

                    b.Property<string>("TwoLetterISO");

                    b.HasKey("Id");

                    b.HasIndex("TwoLetterISO")
                        .HasName("IX_TwoLetterISO");

                    b.ToTable("CmsLanguage","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsMenu_Id_seq");

                    b.Property<int?>("LanguageId");

                    b.Property<string>("Title");

                    b.Property<string>("TwoLetterISO");

                    b.HasKey("Id")
                        .HasName("PK_CmsMenu_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("LanguageId");

                    b.ToTable("CmsMenu","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsMenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsMenuItem_Id_seq");

                    b.Property<int>("Depth");

                    b.Property<string>("Hierarchy");

                    b.Property<int?>("LanguageId");

                    b.Property<int>("MenuId");

                    b.Property<int>("ParentId");

                    b.Property<string>("Title");

                    b.Property<string>("TwoLetterISO");

                    b.HasKey("Id")
                        .HasName("PK_CmsMenuItem_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("LanguageId");

                    b.HasIndex("MenuId");

                    b.ToTable("CmsMenuItem","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsPage_Id_seq");

                    b.Property<int>("Depth");

                    b.Property<string>("Hierarchy");

                    b.Property<int?>("LanguageId");

                    b.Property<int>("ParentId");

                    b.Property<string>("Title");

                    b.Property<string>("TwoLetterISO");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("IX_CmsPage_Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("CmsPage","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsRole_Id_seq");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("Title");

                    b.HasKey("Id")
                        .HasName("PK_CmsRole_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("CmsRole","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsRolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsRolePermission_Id_seq");

                    b.Property<int>("Depth");

                    b.Property<string>("Hierarchy");

                    b.Property<int>("ParentId");

                    b.Property<int>("PermissionOperation");

                    b.Property<string>("Resource");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id")
                        .HasName("PK_CmsRolePermission_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("RoleId");

                    b.ToTable("CmsRolePermission","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsUser_Id_seq");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Surname");

                    b.HasKey("Id")
                        .HasName("PK_CmsUser_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("CmsUser","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsUserPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CmsUserPermission_Id_seq");

                    b.Property<int>("Depth");

                    b.Property<string>("Hierarchy");

                    b.Property<int>("ParentId");

                    b.Property<int>("PermissionOperation");

                    b.Property<string>("Resource");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_CmsUserPermission_Id")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("UserId");

                    b.ToTable("CmsUserPermission","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PK_UserRole_UserId_RoleId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("RoleId");

                    b.ToTable("CmsUserCmsRole","borgdb");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsMenu", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Domain.CmsLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsMenuItem", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Domain.CmsLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("Borg.Platform.EF.CMS.Domain.CmsMenu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Domain.CmsPage", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Domain.CmsLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsRolePermission", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Security.CmsRole", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.CmsUserPermission", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Security.CmsUser", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Borg.Platform.EF.CMS.Security.UserRole", b =>
                {
                    b.HasOne("Borg.Platform.EF.CMS.Security.CmsRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Borg.Platform.EF.CMS.Security.CmsUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
