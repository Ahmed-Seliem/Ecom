using Ecom.CORE.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.INFRASTRUCTURE.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(c => c.PhotoName).IsRequired().HasMaxLength(255);
            builder.Property(c => c.ProductId).IsRequired();
            builder.HasData(new Photo { Id = 2, PhotoName = "testPhoto",  ProductId=1 });
           
        }
    }
}
