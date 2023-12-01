using WebApp.Models;

namespace WebApp.Helpers
{
    public class RosterSorter
    {
        public static string[] SortRoster(PeriodRoster roster) {
            return roster.GetSortedRoster();
        }
    }
}