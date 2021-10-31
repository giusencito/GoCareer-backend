using Gocareer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Infrastructure.Mapping
{
    public class EstudianteMap : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.ToTable("Estudiante");
            builder.HasKey(c => c.UserId);
            builder.Property(c => c.UserId).HasColumnName("UserId")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.UserName)
              .HasColumnName("UserName")
              .HasMaxLength(30)
              .IsUnicode(false)
              .IsRequired();

            builder.Property(c => c.UserLastname)
              .HasColumnName("UserLastname")
              .HasMaxLength(30)
              .IsUnicode(false)
              .IsRequired();

            builder.Property(c => c.Useremail)
              .HasColumnName("Useremail")
              .HasMaxLength(50)
              .IsUnicode(false)
              .IsRequired();

            builder.Property(c => c.UserPassword)
             .HasColumnName("UserPassword")
             .HasMaxLength(50)
             .IsUnicode(false)
             .IsRequired();
        }
    }
}
