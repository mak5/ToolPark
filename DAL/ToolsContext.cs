using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ToolsContext : DbContext
    {
        public ToolsContext(DbContextOptions<ToolsContext> options):base(options)
        {
        }

        public DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tool>()
                .ToTable("Tool")
                .HasKey(t => t.SerialNumber);
        }
    }
}
