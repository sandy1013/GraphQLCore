using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLCore.Data.Models.EntityModels
{
    public partial class GraphQLCoreDbContext: DbContext
    {
        public GraphQLCoreDbContext()
        {
            
        }

        public GraphQLCoreDbContext(DbContextOptions<GraphQLCoreDbContext> options): base(options)
        {
                
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=PRINHYLTPDL0526;Initial Catalog=GraphQLCoreDB;Integrated Security=True");
            }
        }
    }
}
