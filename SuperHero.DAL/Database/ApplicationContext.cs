using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuperHero.DAL.Database
{
    public class ApplicationContext:IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //    //builder.Entity<Post>().Property(e => e.CreatedTime).HasDefaultValueSql("GETDATE()");
            //    //builder.Entity<Catogery>().Property(e => e.createdTime).HasDefaultValueSql("GETDATE()");
            //    //builder.Entity<Comment>().Property(e => e.createdOn).HasDefaultValueSql("GETDATE()");


            //    //Rename Identity
            //    builder.Entity<IdentityUser>().ToTable("Person", "security");
            //    builder.Entity<IdentityRole>().ToTable("Roles", "security");
            //Remove Identity Coloumn
            //builder.Entity<IdentityUser>().Ignore(e => e.A);
            //    //builder.Entity<Person>()
            //    // .Property(et => et.Id)
            //    // .ValueGeneratedOnAdd();
            builder.Entity<Friends>().HasKey(a => new { a.personId, a.FriendId });
            base.OnModelCreating(builder);
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<DoctorInfo> DoctorsInfos { get; set; }
        public DbSet<TrainerInfo> TrainerInfos { get; set; }
        public DbSet<DonnerInfo> DonnerInfos { get; set; }
        public DbSet<Catogery> Catogeries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ReactPost> ReactPosts { get; set; }
        public DbSet<CoursesComment> coursesComments { get; set; }
        public DbSet<PersonGroup> personGroups { get; set; }
        public DbSet<DoctorRating> DoctorRating { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Radiology> Radiologies { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Recorder> Recorders { get; set; }
        public DbSet<MedicalSyndicate> MedicalSyndicates { get; set; }
        public DbSet<BankAccount> bankAccounts { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }

        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
        public DbSet<Problem> Problems { get; set; }
    }
}
