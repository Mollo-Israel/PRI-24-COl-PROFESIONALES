using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoColProfesionales.Models;

namespace ProyectoColProfesionales.Data
{
    public class DataDbContext: DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) 
            : base(options)
        {
            People = Set<Person>();
            Professional = Set<Professional>();
            Users = Set<User>();
            Activity1 = Set<Activity1>();
            Thesis = Set<ThesisModel>();
            Notification = Set<Notification>();
        }
        public DbSet<Activity1> Activity1 { get; set; }
        public DbSet<ActivityProfessional> ActivityProfessional { get; set; }
        public DbSet<ActivityVoucher> ActivityVoucher { get; set; }
        public DbSet<Professional> Professional { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ThesisModel> Thesis { get; set; }
        //DEVON
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notification { get; set; }
        //public DataDbContext(DbContextOptions<DataDbContext> options)
        //     : base(options)
        //{
        //    People = Set<Person>();
        //    Professional = Set<Professional>();
        //    Users = Set<User>();
        //    Activity1 = Set<Activity1>();
        //    Thesis = Set<ThesisModel>();
        //    Notification = Set<Notification>();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales
            modelBuilder.Entity<Activity1>().ToTable("Activity");

            modelBuilder.Entity<ActivityProfessional>().ToTable("ActivityProfessional").HasKey(ap => ap.idActivityProfessional);

            modelBuilder.Entity<ActivityVoucher>().ToTable("ActivityVoucher").Property(av => av.voucherFile).HasColumnType("varbinary(MAX)");

            modelBuilder.Entity<Professional>().ToTable("Professional");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<ThesisModel>().ToTable("Thesis");



            base.OnModelCreating(modelBuilder);
        }
    }
}
