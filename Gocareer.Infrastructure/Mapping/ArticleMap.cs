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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article");
            builder.HasKey(c => c.Articleid);
            builder.Property(c => c.Articleid)
               .HasColumnName("Articleid")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.ArticleName)
                .HasColumnName("ArticleName")
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.ArticleDescription)
                .HasColumnName("ArticleDescription")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.Careerid)
               .HasColumnName("Careerid");

            builder.HasOne(c => c.Career)
                    .WithMany(c => c.Articles)
                    .HasForeignKey(c => c.Careerid);
        }
    }
}
