using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class ResultBuilder
    {
        //Missing logic for SumaCumLaude
        public static DiplomaResult GetDiplomaResult(int average)
        {
            if (average < 50)
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
