using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker.Repositories
{
    public class DiplomaRepo : IDiplomaRepo
    {
        public Diploma[] GetAll()
        {
            return new[]
            {
                new Diploma
                {
                    Id = 1,
                    Credits = 4,
                    Requirements = new int[]{100,102,103,104}
                }
            };
        }

        public Diploma GetById(int id)
        {
            return GetAll()?.FirstOrDefault(d => d.Id == id);
        }
    }
}
