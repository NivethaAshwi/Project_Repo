using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Models;


namespace VisitorManagement.DataAccess
{
    public class VisitorCategoryDBContext: DbContext  
        //dbcontext represents a database connection and a set of tables(bridge between your domain or entity classes and the database)
         {
         //dbcontext
        //DbContext begins when the instance is created and ends when the instance is disposed.
        //A DbContext instance is designed to be used for a single unit-of-work(all the transactions like insert/update/delete and so on are done in one single transaction, rather then doing multiple database transactions.)
        public VisitorCategoryDBContext(DbContextOptions<VisitorCategoryDBContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<VisitorCategoryDetails>()
        .HasMany(e => e.Visitordetails)
       
        .WithOne(e => e.VisitorCategoryDetails)
        .HasForeignKey(e => e.VisitorCategoryId)
        .IsRequired();



            modelBuilder.Entity<Visitordetails>()
        .HasMany(e => e.VisitorLog)
        .WithOne(e => e.visitordetails)
        .HasForeignKey(e => e.VisitorId)
        .IsRequired();


            modelBuilder.Entity<ResidentDetails>()
       .HasMany(e => e.LogDetails)
       .WithOne(e => e.ResidentDetails)
       .HasForeignKey(e => e.ResidentId)
       .IsRequired();
        }
        public DbSet<VisitorCategoryDetails> VisitorCategoryDetail { get; set; }
        public DbSet<Visitordetails> Visitordetails { get; set; }
        public DbSet<ResidentDetails> ResidentDetails { get; set; }
        public DbSet<VisitorLogsDetails> LogDetails { get; set; }
      
    }
}
