using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker.Repositories
{
    public interface IRepo<T>
    {
        T[] GetAll();
        T GetById(int id);
    }
}
