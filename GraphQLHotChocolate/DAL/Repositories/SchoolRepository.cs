using GraphQLHotChocolate.DAL.Context;
using GraphQLHotChocolate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL.Repositories
{
    public class SchoolRepository
    {
        private readonly SchoolDbContext _schoolDbContext;

        public SchoolRepository(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public List<School> GetAllSchoolOnly()
        {
            return _schoolDbContext.Schools.ToList();
        }

        public List<School> GetAllSchoolsWithStudent()
        {
            return _schoolDbContext.Schools
                .Include(d => d.Students)
                .ToList();
        }

        public async Task<School> CreateSchool(School school)
        {
            await _schoolDbContext.Schools.AddAsync(school);
            await _schoolDbContext.SaveChangesAsync();
            return school;
        }
    }
}
