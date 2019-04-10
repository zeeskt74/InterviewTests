using GraduationTracker.Models;

namespace GraduationTracker.Services
{
    public interface IDiplomaService
    {
        Course[] GetDiplomaCoursesByRequirement(Course[] courses, Requirement requirement);
    }
}