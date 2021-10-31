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
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(c => c.Messageid);
            builder.Property(c => c.Messageid).HasColumnName("Messageid")
               .ValueGeneratedOnAdd();
            builder.Property(c => c.MessageDescription)
             .HasColumnName("MessageDescription")
             .HasMaxLength(100)
             .IsUnicode(false)
             .IsRequired();
            builder.Property(c => c.answer)
            .HasColumnName("answer")
            .HasMaxLength(100)
            .IsUnicode(false);
            builder.Property(c => c.UserId)
          .HasColumnName("UserId");
            builder.Property(c => c.EspecialistId)
            .HasColumnName("EspecialistId");

            builder.HasOne(c => c.Estudiante).WithMany(i => i.Messages).HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Especialist).WithMany(i => i.Messages).HasForeignKey(c => c.EspecialistId);

        }
    }
}
