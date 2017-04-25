using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPICore.Models
{
    public partial class WebAPIContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerReport> CustomerReport { get; set; }

        public WebAPIContext(DbContextOptions<WebAPIContext> options)
    : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.EnrolledDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}