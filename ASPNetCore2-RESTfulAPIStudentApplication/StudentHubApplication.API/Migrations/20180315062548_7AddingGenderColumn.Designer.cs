﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StudentHubApplication.API.Entities;
using System;

namespace StudentHubApplication.API.Migrations
{
    [DbContext(typeof(ApplicationInfoContext))]
    [Migration("20180315062548_7AddingGenderColumn")]
    partial class _7AddingGenderColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentHubApplication.API.Entities.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryOfBirthId");

                    b.Property<int>("CountryOfResidenceId");

                    b.Property<DateTimeOffset>("DateOfBirth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CountryOfBirthId");

                    b.HasIndex("CountryOfResidenceId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.ApplicationCourseCampus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<int>("CourseCampusId");

                    b.Property<bool>("IsPrimaryLocation");

                    b.Property<string>("Notes")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("CourseCampusId");

                    b.ToTable("ApplicationCourseCampuses");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.ApplicationQualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<DateTime?>("CompletionDate");

                    b.Property<bool>("IsPrimaryQualification");

                    b.Property<string>("Notes")
                        .HasMaxLength(250);

                    b.Property<int>("QualificationId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("QualificationId");

                    b.ToTable("ApplicationQualifications");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.Campus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CampusCode")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Campuses");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.CourseCampus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CampusId");

                    b.Property<int>("CourseId");

                    b.HasKey("Id");

                    b.HasIndex("CampusId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseCampuses");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.Application", b =>
                {
                    b.HasOne("StudentHubApplication.API.Entities.Country", "CountryOfBirth")
                        .WithMany("ApplicationCountryOfBirth")
                        .HasForeignKey("CountryOfBirthId");

                    b.HasOne("StudentHubApplication.API.Entities.Country", "CountryOfResidence")
                        .WithMany("ApplicationCountryOfResidence")
                        .HasForeignKey("CountryOfResidenceId");
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.ApplicationCourseCampus", b =>
                {
                    b.HasOne("StudentHubApplication.API.Entities.Application", "Application")
                        .WithMany("ApplicationCourseCampuses")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentHubApplication.API.Entities.CourseCampus", "CourseCampus")
                        .WithMany("ApplicationCourseCampuses")
                        .HasForeignKey("CourseCampusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.ApplicationQualification", b =>
                {
                    b.HasOne("StudentHubApplication.API.Entities.Application", "Application")
                        .WithMany("ApplicationQualifications")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentHubApplication.API.Entities.Qualification", "Qualification")
                        .WithMany("ApplicationQualifications")
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentHubApplication.API.Entities.CourseCampus", b =>
                {
                    b.HasOne("StudentHubApplication.API.Entities.Campus", "Campus")
                        .WithMany("CourseCampus")
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentHubApplication.API.Entities.Course", "Course")
                        .WithMany("CourseCampus")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
