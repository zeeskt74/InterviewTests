using GraduationTracker.Models;
using GraduationTracker.Repositories;
using GraduationTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        IDiplomaService _diplomaService;
        IRequirmentRepo _requirmentRepo;

        public GraduationTracker(IDiplomaService diplomaService, IRequirmentRepo requirmentRepo)
        {
            _diplomaService = diplomaService;
            _requirmentRepo = requirmentRepo;
        }

        public GraduationTracker() : this(new DiplomaService(), new RequirmentRepo())
        {
            //poor man's dependency
        }

        public DiplomaResult  HasGraduated(Diploma diploma, Student student)
        {
            if (diploma == null)
            {
                throw new ArgumentNullException($"Argument {nameof(diploma)} cann't be null");
            }
            if (student == null)
            {
                throw new ArgumentNullException($"Argument {nameof(student)} cann't be null");
            }

            var credits = 0;
            var average = 0;
            var totalMarks = 0;
            var passedCourses = 0;

            foreach (var reqId in diploma.Requirements)
            {
                var requirement = _requirmentRepo.GetById(reqId);
                var studentCourses = _diplomaService.GetDiplomaCoursesByRequirement(student.Courses, requirement);

                if (studentCourses.Count() != requirement.CourseCount())
                    return ResultBuilder.GetDiplomaResult(0);

                totalMarks += studentCourses.Sum(c => c.Mark);
                passedCourses = studentCourses.Count(c => c.Mark >= requirement.MinimumMark);

                if (passedCourses == requirement.Credits)
                    credits += requirement.Credits;
            }

            //Student didn't complete all the courses
            if(credits != diploma.Credits)
                return ResultBuilder.GetDiplomaResult(0);

            average = totalMarks / student.Courses.Length;

            return ResultBuilder.GetDiplomaResult(average);
        }
    }
}
    