using WebApp.Helpers;

namespace WebApp.Models
{
    public class PeriodRoster
    {
        public PeriodRoster()
        {
            StudentNames = Array.Empty<string>();
        }
        public int Period { get; set; }
        public string[] StudentNames { get; set; }
        public string[] GetSortedRoster()
        {
            var sortedNames = RosterSorter.GetSortedStudentNames(StudentNames);
            return (from name in sortedNames select $"{name.FirstName} {name.LastName}").ToArray();
            
        }
    }
}