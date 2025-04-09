
using GradutionP.DTO;
using Microsoft.EntityFrameworkCore;
using GradutionP.Models;

namespace GradutionP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; } // إضافة التقييمات

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // العلاقة بين Profile و Patient (واحد لواحد)
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Patient)
                .WithOne(p => p.Profile)
                .HasForeignKey<Profile>(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // العلاقة بين Favorite و (Patient - Doctor)
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Doctor)
                .WithMany(d => d.Favorites)
                .HasForeignKey(f => f.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

         
            // العلاقة بين Availability و Doctor (واحد لمتعدد)
            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
           

            // العلاقة بين Appointment و (Doctor - Patient)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // العلاقة بين feedback و (Doctor - Patient)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany()
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Doctor)
                .WithMany()
                .HasForeignKey(f => f.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
