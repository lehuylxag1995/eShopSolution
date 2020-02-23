﻿using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace eShopSolution.Data.Extension
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(new AppConfig[]
            {
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
            });

            modelBuilder.Entity<Language>().HasData(new Language[]
            {
                new Language(){Id="vi-VN",Name="Tiếng Việt",IsDefault=true},
                new Language(){Id="en-US",Name="English",IsDefault=false}
            });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active,
                }
            });

            modelBuilder.Entity<CategoryTranslation>().HasData(new CategoryTranslation[]
            {
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam",
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt products for men",
                    SeoTitle = "The shirt products for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nữ",
                    SeoTitle = "Sản phẩm áo thời trang women"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "women-shirt",
                    SeoDescription = "The shirt products for women",
                    SeoTitle = "The shirt products for women"
                }
            });

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                OriginalPrice = 100000,
                Price = 200000,
                Stock = 0,
                ViewCount = 0,
            });
            modelBuilder.Entity<ProductTranslation>().HasData(new ProductTranslation[]
            {
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo sơ mi nam trắng Việt Tiến",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                    SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                    SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                    Details = "Áo sơ mi nam trắng Việt Tiến",
                    Description = "Áo sơ mi nam trắng Việt Tiến"
                },
                new ProductTranslation()
                {
                     Id = 2,
                     ProductId = 1,
                     Name = "Viet Tien Men T-Shirt",
                     LanguageId = "en-US",
                     SeoAlias = "viet-tien-men-t-shirt",
                     SeoDescription = "Viet Tien Men T-Shirt",
                     SeoTitle = "Viet Tien Men T-Shirt",
                     Details = "Viet Tien Men T-Shirt",
                     Description = "Viet Tien Men T-Shirt"
                }
            });

            modelBuilder.Entity<ProductInCategory>().HasData(new ProductInCategory()
            {
                ProductId = 1,
                CategoryId = 1,
            });

            var ADMIN_ID = new Guid("D3AC39D2-D541-4CE9-8362-2A92B8AF37E5");
            var ROLE_ID = new Guid("D48F23BB-2138-48DC-81E0-318910AAE84C");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin",
                Description="Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "lehuylxag1995@gmail.com",
                NormalizedEmail = "lehuylxag1995@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = string.Empty,
                FirstName="Huy",
                LastName="Lê",
                Dob=new DateTime(1995,12,15)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });




        }
    }
}
