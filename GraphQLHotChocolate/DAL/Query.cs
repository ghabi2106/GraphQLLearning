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
    public class Query
    {
        public List<Student> AllStudentOnly([Service] StudentRepository studentRepository) =>
            studentRepository.GetStudents();

        public List<Student> AllStudentWithSchool([Service] StudentRepository studentRepository) =>
            studentRepository.GetStudentsWithSchool();

        public async Task<Student> GetStudentById([Service] StudentRepository studentRepository,
            [Service] ITopicEventSender eventSender, int id)
        {
            Student gottenStudent = studentRepository.GetStudentById(id);
            await eventSender.SendAsync("ReturnedStudent", gottenStudent);
            return gottenStudent;
        }

        public List<School> AllSchoolsOnly([Service] SchoolRepository schoolRepository) =>
            schoolRepository.GetAllSchoolOnly();

        public List<School> AllSchoolsWithStudent([Service] SchoolRepository schoolRepository) =>
            schoolRepository.GetAllSchoolsWithStudent();
    }
}
