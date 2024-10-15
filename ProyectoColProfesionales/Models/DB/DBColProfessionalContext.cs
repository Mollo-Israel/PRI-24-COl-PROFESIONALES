using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoColProfesionales.Models.DB
{
    public partial class DBColProfessionalContext : DbContext
    {
        public DBColProfessionalContext()
        {
        }

        public DBColProfessionalContext(DbContextOptions<DBColProfessionalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityProfessional> ActivityProfessionals { get; set; } = null!;
        public virtual DbSet<ActivityVoucher> ActivityVouchers { get; set; } = null!;
        public virtual DbSet<Letter> Letters { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Professional> Professionals { get; set; } = null!;
        public virtual DbSet<Thesis> Theses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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
                entity.HasKey(e => e.IdActivity);

                entity.ToTable("Activity");

                entity.Property(e => e.IdActivity).HasColumnName("idActivity");

                entity.Property(e => e.Auditorium)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("auditorium");

                entity.Property(e => e.DateActivity)
                    .HasColumnType("datetime")
                    .HasColumnName("dateActivity");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.HasAssistance).HasColumnName("hasAssistance");

                entity.Property(e => e.HasPayment).HasColumnName("hasPayment");

                entity.Property(e => e.IdThesis).HasColumnName("idThesis");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("longitude");

                entity.Property(e => e.Place)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("place");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateActivity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stateActivity");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdThesisNavigation)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.IdThesis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_Thesis");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.ActivityIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Activity_User5");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.ActivityIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Activity_User4");
            });

            modelBuilder.Entity<ActivityProfessional>(entity =>
            {
                entity.HasKey(e => e.IdActivityProfessional);

                entity.ToTable("ActivityProfessional");

                entity.Property(e => e.IdActivityProfessional).HasColumnName("idActivityProfessional");

                entity.Property(e => e.IdActivity).HasColumnName("idActivity");

                entity.Property(e => e.IdProfessional).HasColumnName("idProfessional");

                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.ActivityProfessionals)
                    .HasForeignKey(d => d.IdActivity)
                    .HasConstraintName("FK_ActivityProfessional_Activity");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.ActivityProfessionals)
                    .HasForeignKey(d => d.IdProfessional)
                    .HasConstraintName("FK_ActivityProfessional_Professional");
            });

            modelBuilder.Entity<ActivityVoucher>(entity =>
            {
                entity.HasKey(e => e.IdActivityVoucher);

                entity.ToTable("ActivityVoucher");

                entity.Property(e => e.IdActivityVoucher).HasColumnName("idActivityVoucher");

                entity.Property(e => e.IdActivity).HasColumnName("idActivity");

                entity.Property(e => e.NameFile)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nameFile");

                entity.Property(e => e.VoucherFile).HasColumnName("voucherFile");

                entity.Property(e => e.VoucherType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("voucherType");

                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.ActivityVouchers)
                    .HasForeignKey(d => d.IdActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityVoucher_Activity");
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.HasKey(e => e.IdLetter);

                entity.ToTable("Letter");

                entity.Property(e => e.IdLetter).HasColumnName("idLetter");

                entity.Property(e => e.Format)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("format");

                entity.Property(e => e.IdActivity).HasColumnName("idActivity");

                entity.Property(e => e.IdProfessional).HasColumnName("idProfessional");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdActivityNavigation)
                    .WithMany(p => p.Letters)
                    .HasForeignKey(d => d.IdActivity)
                    .HasConstraintName("FK_Letter_Activity");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.Letters)
                    .HasForeignKey(d => d.IdProfessional)
                    .HasConstraintName("FK_Letter_Professional1");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.LetterIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Letter_User3");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.LetterIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Letter_User2");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.IdNotification);

                entity.ToTable("Notification");

                entity.Property(e => e.IdNotification).HasColumnName("idNotification");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTime");

                entity.Property(e => e.IdLetter).HasColumnName("idLetter");

                entity.Property(e => e.IdProfessional).HasColumnName("idProfessional");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdLetterNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdLetter)
                    .HasConstraintName("FK_Notification_Letter");

                entity.HasOne(d => d.IdProfessionalNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdProfessional)
                    .HasConstraintName("FK_Notification_Professional1");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.NotificationIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Notification_User3");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.NotificationIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Notification_User2");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson);

                entity.ToTable("Person");

                entity.Property(e => e.IdPerson).HasColumnName("idPerson");

                entity.Property(e => e.Email)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("identityNumber");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Names)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("names");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("secondLastName");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.PersonIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Person_User3");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.PersonIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Person_User2");
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.HasKey(e => e.IdProfessional);

                entity.ToTable("Professional");

                entity.Property(e => e.IdProfessional).HasColumnName("idProfessional");

                entity.Property(e => e.Carrera)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("carrera");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.IdPerson).HasColumnName("idPerson");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Specialty)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("specialty");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Universidad)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("universidad");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professional_Person1");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.ProfessionalIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Professional_User3");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.ProfessionalIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Professional_User2");
            });

            modelBuilder.Entity<Thesis>(entity =>
            {
                entity.HasKey(e => e.IdThesis);

                entity.ToTable("Thesis");

                entity.Property(e => e.IdThesis).HasColumnName("idThesis");

                entity.Property(e => e.Career)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("career");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Student)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student");

                entity.Property(e => e.ThesisPdf)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("thesisPDF");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.IdUserLastUpdateNavigation)
                    .WithMany(p => p.ThesisIdUserLastUpdateNavigations)
                    .HasForeignKey(d => d.IdUserLastUpdate)
                    .HasConstraintName("FK_Thesis_User3");

                entity.HasOne(d => d.IdUserRegisterNavigation)
                    .WithMany(p => p.ThesisIdUserRegisterNavigations)
                    .HasForeignKey(d => d.IdUserRegister)
                    .HasConstraintName("FK_Thesis_User2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdPerson).HasColumnName("idPerson");

                entity.Property(e => e.IdUserLastUpdate).HasColumnName("idUserLastUpdate");

                entity.Property(e => e.IdUserRegister).HasColumnName("idUserRegister");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Password)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
