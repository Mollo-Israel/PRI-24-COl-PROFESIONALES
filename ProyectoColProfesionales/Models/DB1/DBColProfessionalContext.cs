using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoColProfesionales.Models.DB;

namespace ProyectoColProfesionales.Models.DB1
{
    public partial class DBColProfessionalContext : DbContext
    {
        public DBColProfessionalContext()
        {
        }

        public DBColProfessionalContext(DbContextOptions<DBColProfessionalContext> options)
            : base(options)
        {
            People = Set<Person>();
            Professionals = Set<Professional>();
            Users = Set<User>();
            Activities = Set<Activity>();
            Theses = Set<Thesis>();
            Notification = Set<Notification>();
        }
        public DbSet<Notification> Notification { get; set; }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityProfessional> ActivityProfessionals { get; set; } = null!;
        public virtual DbSet<ActivityVoucher> ActivityVouchers { get; set; } = null!;
        public virtual DbSet<Letter> Letters { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Professional> Professionals { get; set; } = null!;
        public virtual DbSet<Thesis> Theses { get; set; } = null!;
        public virtual DbSet<ThesisFile> ThesisFiles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        //agregado
        public virtual DbSet<Notification2> Notifications2 { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=DBColProfessional;User Id=sa;Password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdThesisNavigation)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.IdThesis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Thesis");
            });

            modelBuilder.Entity<ActivityProfessional>(entity =>
            {
                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.ActivityProfessionals)
                    .HasForeignKey(d => d.IdActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityProfessional_Activity");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.ActivityProfessionals)
                    .HasForeignKey(d => d.IdProfessional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityProfessional_Professional");
            });

            modelBuilder.Entity<ActivityVoucher>(entity =>
            {
                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.ActivityVouchers)
                    .HasForeignKey(d => d.IdActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityVoucher_Activity");
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.Letters)
                    .HasForeignKey(d => d.IdActivity)
                    .HasConstraintName("FK_Letter_Activity");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.Letters)
                    .HasForeignKey(d => d.IdProfessional)
                    .HasConstraintName("FK_Letter_Professional1");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdLetterNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdLetter)
                    .HasConstraintName("FK_Notification_Letter");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdProfessional)
                    .HasConstraintName("FK_Notification_Professional1");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professional_Person1");
            });

            modelBuilder.Entity<Thesis>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ThesisFile>(entity =>
            {
                entity.HasOne(d => d.IdThesisNavigation)
                    .WithMany(p => p.ThesisFiles)
                    .HasForeignKey(d => d.IdThesis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThesisFile_Thesis");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Person");
            });

            //agregado
            modelBuilder.Entity<Notification2>(entity =>
            {
                entity.HasOne(n => n.Person)
                      .WithMany(p => p.Notifications2)
                      .HasForeignKey(n => n.IdPerson)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Notification2_Person");

                entity.Property(n => n.Status)
                      .HasDefaultValue(1)
                      .ValueGeneratedOnAdd(); // Se asegura que el valor predeterminado de Status sea 1
            });
            //agregado
            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
