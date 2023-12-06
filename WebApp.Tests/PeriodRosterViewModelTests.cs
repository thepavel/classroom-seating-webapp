using WebApp.Models;
using WebApp.Pages;

namespace WebApp.Tests
{
    public class PeriodRosterViewModelTests
    {

        private static PeriodRoster _defaultRoster = new PeriodRoster() { Period = 1, StudentNames = new string[] { "a a", "b b", "c c" } };

        [Fact]
        public void PeriodRosterViewModelSetsSortedNamesFromPeriodRoster()
        {
            var prvm = new PeriodRosterViewModel(_defaultRoster);

            Action<string> existsInOriginalList = (a) => 
            {
                Assert.Contains(a, prvm.SortedNames);
            };
            Assert.All(_defaultRoster.StudentNames, existsInOriginalList);

        }

    }
}