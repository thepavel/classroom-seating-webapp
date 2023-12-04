using WebApp.Models;

namespace WebApp.Helpers
{
    public class RosterSorter
    {

        public static StudentName[] GetSortedStudentNames(string[] names) {
            return (from name in names 
                        let studentName = NameParser.FromString(name)
                        orderby studentName.LastName descending
                        select studentName).ToArray();
        }

        public static string[] GetSortedStudents(string[] names)
        {
            return (from name in names
                    let parsedName = name.Split(" ")
                    let studentName = new StudentName(parsedName.First(), parsedName.Last())
                    orderby studentName.LastName descending
                    select $"{studentName.FirstName} {studentName.LastName}"
                    ).ToArray();

        }

        public static string[] SortStudentNames(PeriodRoster roster)
        {
            
            return roster.GetSortedRoster();
        }
    }
}