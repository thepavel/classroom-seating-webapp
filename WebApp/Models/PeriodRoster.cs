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
            return (from name in (StudentName[]?)RosterSorter.GetSortedStudentNames(StudentNames) select $"{name.FirstName} {name.LastName}").ToArray();
        }
    }
}