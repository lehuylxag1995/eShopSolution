using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();
            builder.Property(x => x.SeoAlias)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .HasMaxLength(200);
            builder.Property(x => x.LanguageId)
                .IsUnicode(false)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTranslations)
                .HasForeignKey(pt => pt.ProductId);
            builder.HasOne(pt => pt.Language)
                .WithMany(l => l.ProductTranslations)
                .HasForeignKey(pt => pt.LanguageId);
        }
    }
}
