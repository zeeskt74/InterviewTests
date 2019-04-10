using GraduationTracker.Models;
using GraduationTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {   
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
            var reqRepo = new RequirmentRepo();

            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                for(int j = 0; j < student.Courses.Length; j++)
                {
                    var requirement = reqRepo.GetById(diploma.Requirements[i]);

                    for (int k = 0; k < requirement.Courses.Length; k++)
                    {
                        if (requirement.Courses[k] == student.Courses[j].Id)
                        {
                            average += student.Courses[j].Mark;
                            if (student.Courses[j].Mark > requirement.MinimumMark)
                            {
                                credits += requirement.Credits;
                            }
                        }
                    }
                }
            }

            average = average / student.Courses.Length;

            return ResultBuilder.GetDiplomaResult(average);
        }
    }
}
    