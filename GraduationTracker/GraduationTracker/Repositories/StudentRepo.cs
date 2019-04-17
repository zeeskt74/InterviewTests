using GraduationTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker.Repositories
{
    public class StudentRepo : IStudentRepo
    {

        public Student[] FilterStudentByCourses(Func<Course, bool> predicate)
        {
            var studentList = new List<Student>();
            foreach (var s in GetAll())
            {
                var course = GetCourses(s, predicate);

                if (course.Any())
                {
                    studentList.Add(s);
                }
            }
            return studentList.ToArray();
        }


        public Course[] GetCourses(Student student1, Func<Course, bool> predicate)
        {
            var courseList = new List<Course>();
            return student1.Courses.Where(predicate).ToArray();
            
        }

        public Student[] GetMathStudents()
        {
            return FilterStudentByCourses(c => c.Name == "Math" && c.Mark >= 50);
        }


        public Student[] GetFailedScienceStudents()
        {
            return FilterStudentByCourses(c => c.Name == "Science" && c.Mark < 50);
        }


        public Student[] GetHighestGradsStudents()
        {
            var studentList = GetAll().ToList();

            foreach (var s in studentList)
            {
                var maxMarks = s.Courses.Max(c => c.Mark);

                s.Courses.ToList().RemoveAll(c => c.Mark != maxMarks);

            }

            return studentList.ToArray();
        }


        public Student[] GetAll()
        {
            return new[]
            {
               new Student
               {
                   Id = 1,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark = 95 },
                        new Course{Id = 2, Name = "Science", Mark = 40 },
                        new Course{Id = 3, Name = "Literature", Mark = 60 },
                        new Course{Id = 4, Name = "Physichal Education", Mark = 23 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark = 80 },
                        new Course{Id = 2, Name = "Science", Mark = 49 },
                        new Course{Id = 3, Name = "Literature", Mark = 68 },
                        new Course{Id = 4, Name = "Physichal Education", Mark = 55 }
                   }
               },
                new Student
                {
                    Id = 3,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark= 70 },
                        new Course{Id = 2, Name = "Science", Mark= 90 },
                        new Course{Id = 3, Name = "Literature", Mark= 20 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=60 }
                    }
                },
                new Student
                {
                    Id = 4,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark= 50 },
                        new Course{Id = 2, Name = "Science", Mark=30 },
                        new Course{Id = 3, Name = "Literature", Mark=20 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=10 }
                    }
                }
            };
        }

        public Student GetById(int id)
        {
            return GetAll()?.FirstOrDefault(s => s.Id == id);
        }
    }
}
