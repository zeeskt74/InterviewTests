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

            if(average == 0)
                return new DiplomaResult { Status = false, Standing = STANDING.None };
            else if (average < 50)
                return new DiplomaResult { Status = false, Standing = STANDING.Remedial };
            else if (average < 80)
                return new DiplomaResult { Status = true, Standing = STANDING.Average };
            else if (average < 95)
                return new DiplomaResult { Status = true, Standing = STANDING.MagnaCumLaude };
            else
                return new DiplomaResult { Status = true, Standing = STANDING.MagnaCumLaude }; 
        }
    }
}
    