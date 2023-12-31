using WebApp.Models;
using WebApp.Pages;
using Shouldly;

namespace WebApp.Tests
{
    public class PeriodRosterViewModelTests
    {

        private static PeriodRoster _defaultRoster = new PeriodRoster() { Period = 1, StudentNames = new string[] { "a a", "b b", "c c" } };

        [Fact]
        public void PeriodRosterViewModelSetsSortedNamesFromPeriodRoster()
        {
            var prvm = new PeriodRosterViewModel(_defaultRoster);
            
            prvm.SortedNames.ShouldContain("a a");
            prvm.SortedNames.ShouldContain("b b");
            prvm.SortedNames.ShouldContain("c c");

        }

        [Fact]
        public void PeriodRosterViewModel_SortedNames_AreSameLengthAsStudentNames() 
        {
            var prvm = new PeriodRosterViewModel(_defaultRoster);
            Assert.Equal(_defaultRoster.StudentNames.Length, prvm.SortedNames.Length);
        }

        [Fact]
        public void PeriodRosterViewModel_SortedNames_AreSortedByLastNameDesc()
        {
            var prvm = new PeriodRosterViewModel(_defaultRoster);
            Assert.Equal("c c", prvm.SortedNames.First());
            Assert.Equal("b b", prvm.SortedNames[1]);
            Assert.Equal("a a", prvm.SortedNames.Last());
        }

    }
}