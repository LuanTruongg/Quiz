﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz.Repository;

#nullable disable

namespace Quiz.Repository.Migrations
{
    [DbContext(typeof(QuizDbContext))]
    [Migration("20231107034141_fix_duplicate_Test_of_UserTest")]
    partial class fix_duplicate_Test_of_UserTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                            RoleId = "8d04dce2-969a-435d-bba4-df3f325983dc"
                        },
                        new
                        {
                            UserId = "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                            RoleId = "69bd714f-9576-45ba-b5b7-f00649be00de"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments", (string)null);

                    b.HasData(
                        new
                        {
                            DepartmentId = "SP&KHCB",
                            Name = "Khoa Sư phạm và Khoa học cơ bản"
                        },
                        new
                        {
                            DepartmentId = "KTL",
                            Name = "Khoa Kinh tế - Luật"
                        },
                        new
                        {
                            DepartmentId = "LLCTGDQP&TC",
                            Name = "Khoa Lý luận chính trị - GD Quốc phòng và Thể chất"
                        },
                        new
                        {
                            DepartmentId = "KTCN",
                            Name = "Khoa Kỹ thuật Công nghệ"
                        },
                        new
                        {
                            DepartmentId = "NN&CNTP",
                            Name = "Khoa Nông nghiệp và Công nghệ Thực phẩm"
                        });
                });

            modelBuilder.Entity("Quiz.Repository.Model.Major", b =>
                {
                    b.Property<string>("MajorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Majors", (string)null);

                    b.HasData(
                        new
                        {
                            MajorId = "CNTT",
                            DepartmentId = "KTCN",
                            Name = "Công nghệ thông tin"
                        },
                        new
                        {
                            MajorId = "DK&TDH",
                            DepartmentId = "KTCN",
                            Name = "Điều khiển và tự động hoá"
                        });
                });

            modelBuilder.Entity("Quiz.Repository.Model.MajorSubject", b =>
                {
                    b.Property<string>("MajorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MajorId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("MajorSubjects", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.Module", b =>
                {
                    b.Property<string>("ModuleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ModuleId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Modules", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Difficult")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModuleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionCustom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.Subject", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("General")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.TestStructure", b =>
                {
                    b.Property<string>("TestStructureId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfQuestions")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.HasKey("TestStructureId");

                    b.ToTable("TestStructures", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.TestSubject", b =>
                {
                    b.Property<string>("TestSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TestStructureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TestSubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestSubjectId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestStructureId");

                    b.ToTable("TestSubjects", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("Money")
                        .HasColumnType("real");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                            AccessFailedCount = 0,
                            Address = "Bến Tre",
                            CCCD = "202011237083",
                            ConcurrencyStamp = "e5b0bd38-6765-4949-866e-7840ffc3291b",
                            DoB = new DateTime(2002, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "luantruong020302@gmail.com",
                            EmailConfirmed = false,
                            Fullname = "Trương Hoàng Luân",
                            LockoutEnabled = false,
                            Money = 1000000f,
                            NormalizedEmail = "luantruong020302@gmail.com,",
                            NormalizedUserName = "luantruong020302@gmail.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEGdtDDJ5Pb2mFsBjsISa3B7I05tyme8N1gmz/VSEel0dUsTFpbpzKW6RypuFqL1sew==",
                            PhoneNumber = "0836151007",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Sex = true,
                            TwoFactorEnabled = false,
                            UserName = "luantruong020302@gmail.com"
                        });
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserAnswer", b =>
                {
                    b.Property<string>("UserAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAnswerQuestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserAnswerId");

                    b.HasIndex("UserTestId");

                    b.ToTable("UserAnswers", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserSubject", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("UserSubjects", (string)null);
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserTest", b =>
                {
                    b.Property<string>("UserTestId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("int");

                    b.Property<decimal>("Score")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("TestStructureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserTestId");

                    b.HasIndex("TestStructureId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTests", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<string>");

                    b.HasDiscriminator().HasValue("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "69bd714f-9576-45ba-b5b7-f00649be00de",
                            ConcurrencyStamp = "e588f3c4-d78b-4069-a231-4a56491604de",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "8d04dce2-969a-435d-bba4-df3f325983dc",
                            ConcurrencyStamp = "2dfe4474-856b-4d55-a4cb-becb0416e677",
                            Name = "Teacher",
                            NormalizedName = "Teacher"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Quiz.Repository.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Quiz.Repository.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.Repository.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Quiz.Repository.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quiz.Repository.Model.Major", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Department", "Department")
                        .WithMany("Majors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Quiz.Repository.Model.MajorSubject", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Major", "Major")
                        .WithMany("MajorSubjects")
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.Repository.Model.Subject", "Subject")
                        .WithMany("MajorSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Module", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Subject", "Subject")
                        .WithMany("Modules")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Question", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Module", "Module")
                        .WithMany("Questions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Quiz.Repository.Model.TestSubject", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Question", "Question")
                        .WithMany("TestSubjects")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.Repository.Model.TestStructure", "TestStructure")
                        .WithMany("TestSubjects")
                        .HasForeignKey("TestStructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("TestStructure");
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserAnswer", b =>
                {
                    b.HasOne("Quiz.Repository.Model.UserTest", "UserTest")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTest");
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserSubject", b =>
                {
                    b.HasOne("Quiz.Repository.Model.Subject", "Subject")
                        .WithMany("UserSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.Repository.Model.User", "User")
                        .WithMany("UserSubjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserTest", b =>
                {
                    b.HasOne("Quiz.Repository.Model.TestStructure", "TestStructure")
                        .WithMany("UserTest")
                        .HasForeignKey("TestStructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.Repository.Model.User", "User")
                        .WithMany("UserTests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestStructure");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Department", b =>
                {
                    b.Navigation("Majors");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Major", b =>
                {
                    b.Navigation("MajorSubjects");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Module", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Question", b =>
                {
                    b.Navigation("TestSubjects");
                });

            modelBuilder.Entity("Quiz.Repository.Model.Subject", b =>
                {
                    b.Navigation("MajorSubjects");

                    b.Navigation("Modules");

                    b.Navigation("UserSubjects");
                });

            modelBuilder.Entity("Quiz.Repository.Model.TestStructure", b =>
                {
                    b.Navigation("TestSubjects");

                    b.Navigation("UserTest");
                });

            modelBuilder.Entity("Quiz.Repository.Model.User", b =>
                {
                    b.Navigation("UserSubjects");

                    b.Navigation("UserTests");
                });

            modelBuilder.Entity("Quiz.Repository.Model.UserTest", b =>
                {
                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
