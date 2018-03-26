using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class ApplicationInfoContext : DbContext
    {
        public ApplicationInfoContext(DbContextOptions<ApplicationInfoContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Qualification> Qualifications { get; set; }

        public DbSet<ApplicationQualification> ApplicationQualifications { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<CourseCampus> CourseCampuses { get; set; }

        public DbSet<ApplicationCourseCampus> ApplicationCourseCampuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Fluent API via DBContext

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasOne(d => d.CountryOfResidence)
                    .WithMany(p => p.ApplicationCountryOfResidence)
                    .HasForeignKey(d => d.CountryOfResidenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK__Applicati__Count__25869641");

                entity.HasOne(d => d.CountryOfBirth)
                    .WithMany(p => p.ApplicationCountryOfBirth)
                    .HasForeignKey(d => d.CountryOfBirthId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    //.HasConstraintName("FK__Applicati__Count__267ABA7A");
            });


            modelBuilder.Entity<ApplicationCourseCampus>(entity =>
            {
                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationCourseCampuses)
                    .HasForeignKey(d => d.ApplicationId);
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK__Applicati__Appli__2D27B809");

                entity.HasOne(d => d.CourseCampus)
                    .WithMany(p => p.ApplicationCourseCampuses)
                    .HasForeignKey(d => d.CourseCampusId);
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    //.HasConstraintName("FK__Applicati__Cours__2E1BDC42");
            });

            modelBuilder.Entity<ApplicationQualification>(entity =>
            {
                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationQualifications)
                    .HasForeignKey(d => d.ApplicationId);
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    //.HasConstraintName("FK__Applicati__Appli__32E0915F");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.ApplicationQualifications)
                    .HasForeignKey(d => d.QualificationId);
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    //.HasConstraintName("FK__Applicati__Quali__33D4B598");
            });

            modelBuilder.Entity<CourseCampus>(entity =>
            {
                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.CourseCampus)
                    .HasForeignKey(d => d.CampusId);
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK__CourseCam__Campu__2A4B4B5E");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseCampus)
                    .HasForeignKey(d => d.CourseId);
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    //.HasConstraintName("FK__CourseCam__Cours__29572725");
            });
        }
    }
}
