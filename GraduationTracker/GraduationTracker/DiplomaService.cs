using GraduationTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class DiplomaService : IDiplomaService
    {
        public Course[] GetDiplomaCoursesByRequirement(Course[] courses, Requirement requirement)
        {
            return courses.Where(c => requirement.Courses.Contains(c.Id)).ToArray();
        }

        public int GetDiplomaCourseCountByRequirment(Requirement requirement)
        {
            return requirement.Courses.Count();
        }
    }
}
