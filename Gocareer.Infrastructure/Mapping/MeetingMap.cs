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
    public class MeetingMap : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meeting");
            builder.HasKey(c => c.MeetingId);
            builder.Property(c => c.MeetingId).HasColumnName("MeetingId")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.Date)
             .HasColumnName("Date").IsRequired();

            builder.Property(c => c.Hour)
             .HasColumnName("Hour").IsRequired();

            builder.Property(c => c.UserId)
            .HasColumnName("UserId");
            builder.Property(c => c.EspecialistId)
            .HasColumnName("EspecialistId");

            builder.HasOne(c => c.estudiante).WithMany(i => i.Meetings).HasForeignKey(c=>c.UserId);
            builder.HasOne(c => c.especialist).WithMany(i => i.Meetings).HasForeignKey(c => c.EspecialistId);

        }
    }
}
