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

    }
}