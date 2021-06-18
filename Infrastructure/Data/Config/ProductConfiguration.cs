using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");  //no.of decimal places set to 2. SQLite doesnt have support for decimal but it doesnt cause any problem. it will be used when we replace SQLite by production server eg. MySql
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.ProductBrand).WithMany()
                .HasForeignKey(p =>p.ProductBrandId);  //each product has 1 brand. Each brand can be associated with many products. and can also specify foreign key
            builder.HasOne(t =>t.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);   //adding F.K. is not necessary because EF already take care of it. here its just for practise
        }
    }
}