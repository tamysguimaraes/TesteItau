using Microsoft.EntityFrameworkCore;
using Products.Data.Entities;

namespace Products.Data.Context
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductEntity> ProductEntity { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductEntity>(prod =>
            {
                prod.HasKey(p => new { p.Id }).HasName("PK_Product");


                prod.Property(p => p.Id)
                        .HasColumnType("integer")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                prod.Property(p => p.cBarCode)
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14)
                        .IsRequired();


                prod.Property(p => p.cName)
                        .HasColumnType("varchar(200)")
                        .IsRequired(false);

                prod.Property(p => p.cCategory)
                        .HasColumnType("varchar(200)")
                        .IsRequired(false);

                prod.Property(p => p.nValue)
                        .HasColumnType("numeric(18,2)")
                        .HasDefaultValue(0.00);
            }
            );

        }
    }
}
