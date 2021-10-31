using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gocareer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Gocareer.Infrastructure.Mapping
{
    public class User_TestMap:IEntityTypeConfiguration<User_Test>
    {
        public void Configure(EntityTypeBuilder<User_Test> builder)
        {
            builder.ToTable("User_Test");
            builder.HasKey(c => new { c.UserId, c.Testid, c.Careerid });

            builder.Property(c => c.UserId)
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(c => c.Testid)
               .HasColumnName("Testid")
               .IsRequired();

            builder.Property(c => c.Careerid)
               .HasColumnName("Careerid")
               .IsRequired();

            builder.Property(c => c.releasedate)
                .HasColumnName("Releasedate")
                .IsRequired();

            builder.Property(c => c.Points)
                .HasColumnName("Points")
                .IsRequired();

            builder.HasOne(c => c.Estudiante)
                    .WithMany(c => c.User_Tests)
                    .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.Test)
                    .WithMany(c => c.User_Tests)
                    .HasForeignKey(c => c.Testid);

            builder.HasOne(c => c.Career)
                    .WithMany(c => c.User_Tests)
                    .HasForeignKey(c => c.Careerid);

        }


    }
}
