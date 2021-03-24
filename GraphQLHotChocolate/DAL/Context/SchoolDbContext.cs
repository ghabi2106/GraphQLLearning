using GraphQLHotChocolate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL.Context
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<School> Schools { get; set; }

    }
}
