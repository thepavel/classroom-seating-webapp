using WebApp.Models;

namespace WebApp.Helpers
{
    public class RosterSorter
    {

        public static string[] GetSortedStudents(string[] names)
        {
            return (from name in names
                    let parsedName = name.Split(" ")
                    let studentName = new StudentName(parsedName.First(), parsedName.Last())
                    orderby studentName.LastName descending
                    select $"{studentName.FirstName} {studentName.LastName}"
                    ).ToArray();

        }

/// <summary>
/// Deprecated
/// </summary>
/// <param name="studentNames"></param>
/// <returns></returns>
        public static List<StudentName> SortStudentNamesForSeatAssignment(List<StudentName> studentNames) =>
            studentNames.OrderByDescending(sn => sn.LastName).ToList();

/// <summary>
/// Deprecated
/// </summary>
/// <param name="names"></param>
/// <returns></returns>
        public static string[] SortForSeatAssignment(string[] names)
        {
            return (from name in names
                    let parsedName = name.Split(" ")
                    let studentName = new StudentName(parsedName.First(), parsedName.Last())
                    select $"{parsedName.Last()} {parsedName.First()}")
                                .OrderDescending().ToArray();

        }

        public static string[] SortStudentNames(PeriodRoster roster)
        {
            
            return roster.GetSortedRoster();
        }
    }
}