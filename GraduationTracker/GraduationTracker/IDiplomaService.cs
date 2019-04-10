using GraduationTracker.Models;

namespace GraduationTracker
{
    public interface IDiplomaService
    {
        Course[] GetDiplomaCoursesByRequirement(Course[] courses, Requirement requirement);

        int GetDiplomaCourseCountByRequirment(Requirement requirement);
    }
}