using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(x => new { x.CategoryId, x.ProductId });
            builder.HasOne(pic => pic.Category)
                .WithMany(c => c.ProductInCategories)
                .HasForeignKey(pic=>pic.CategoryId);
            builder.HasOne(pic => pic.Product)
                .WithMany(p => p.ProductInCategories)
                .HasForeignKey(pic => pic.ProductId);
        }
    }
}
