using Gocareer.Infrastructure.Mapping;
using Gocareer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Infrastructure
{
    public class DbContextGocareer : DbContext
    {
        public DbContextGocareer(DbContextOptions<DbContextGocareer> options) : base(options)
        {
        }
        public DbSet<Especialist> Especialists { set; get; }
        public DbSet<Estudiante> Estudiantes { set; get; }
        public DbSet<Meeting> Meetings { set; get; }
        public DbSet<Article> Articles { set; get; }
        public DbSet<Career> Careers { set; get; }
        public DbSet<Message> Messages { set; get; }
        public DbSet<Option> Options { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<Test> Tests { set; get; }
        public DbSet<User_Test> User_Tests { set; get; }
        public DbSet<Work> Works { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EstudianteMap());
            modelBuilder.ApplyConfiguration(new EspecialistMap());
            modelBuilder.ApplyConfiguration(new MeetingMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CareerMap());
            modelBuilder.ApplyConfiguration(new MessageMap());
            modelBuilder.ApplyConfiguration(new OptionMap());
            modelBuilder.ApplyConfiguration(new QuestionMap());
            modelBuilder.ApplyConfiguration(new TestMap());
            modelBuilder.ApplyConfiguration(new User_TestMap());
            modelBuilder.ApplyConfiguration(new WorkMap());
        }

    }
}
