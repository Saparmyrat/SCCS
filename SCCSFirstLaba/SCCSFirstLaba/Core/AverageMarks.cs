using SCCSFirstLaba.Models;
using System.Collections.Generic;
using System.Linq;

namespace SCCSFirstLaba.Core
{
    public static class AverageMarks
    {
        public static double GetAverageStudentMark(this Student student) => student.Marks.Average();

        public static double GetAverageStudentsMark(this IEnumerable<Student> students)
            => students.Select(s => s.GetAverageStudentMark()).Average();
    }
}
