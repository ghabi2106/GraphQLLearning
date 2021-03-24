using GraphQLHotChocolate.DAL.Context;
using GraphQLHotChocolate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL.Repositories
{

    public class StudentRepository
    {
        private readonly SchoolDbContext _schoolDbContext;

        public StudentRepository(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public List<Student> GetStudents()
        {
            return _schoolDbContext.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            var student = _schoolDbContext.Students
                .Include(e => e.School)
                .Where(e => e.StudentId == id)
                .FirstOrDefault();

            if (student != null)
                return student;

            return null;
        }

        public List<Student> GetStudentsWithSchool()
        {
            return _schoolDbContext.Students
                .Include(e => e.School)
                .ToList();
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await _schoolDbContext.Students.AddAsync(student);
            await _schoolDbContext.SaveChangesAsync();
            return student;
        }
    }
}
