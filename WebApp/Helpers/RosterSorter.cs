using WebApp.Models;

namespace WebApp.Helpers
{
    public class RosterSorter
    {
        public static string[] SortStudentNames(PeriodRoster roster) {
            return roster.GetSortedRoster();
        }
    }
}