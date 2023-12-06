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

            Assert.All(_defaultRoster.StudentNames, (a) =>
            {
                Assert.Contains(a, prvm.SortedNames);
            });

        }

        [Fact]
        public void PeriodRosterViewModel_SortedNames_AreSameLengthAsStudentNames() 
        {
            var prvm = new PeriodRosterViewModel(_defaultRoster);
            Assert.Equal(_defaultRoster.StudentNames.Length, prvm.SortedNames.Length);
        }

    }
}