using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Concrete.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);

            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(250);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleID);

            builder.ToTable("Comments");
        }
    }
}
