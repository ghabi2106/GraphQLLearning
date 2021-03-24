using GraphQLHotChocolate.DAL.Entities;
using GraphQLHotChocolate.DAL.Repositories;
using HotChocolate;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL
{
    public class Mutation
    {
        public async Task<School> CreateSchool([Service] SchoolRepository schoolRepository,
            [Service] ITopicEventSender eventSender, string schoolName)
        {
            var newSchool = new School
            {
                Name = schoolName
            };
            var createdSchool = await schoolRepository.CreateSchool(newSchool);

            await eventSender.SendAsync("SchoolCreated", createdSchool);

            return createdSchool;
        }

        public async Task<Student> CreateStudentWithSchoolId([Service] StudentRepository studentRepository,
            string name, int age, string email, int schoolId)
        {
            Student newStudent = new Student
            {
                Name = name,
                Age = age,
                Email = email,
                SchoolId = schoolId
            };

            var createdStudent = await studentRepository.CreateStudent(newStudent);
            return createdStudent;
        }

        public async Task<Student> CreateStudentWithSchool([Service] StudentRepository studentRepository,
            string name, int age, string email, string schoolName)
        {
            Student newStudent = new Student
            {
                Name = name,
                Age = age,
                Email = email,
                School = new School { Name = schoolName }
            };

            var createdStudent = await studentRepository.CreateStudent(newStudent);
            return createdStudent;
        }
    }
}
