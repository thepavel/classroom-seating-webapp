using WebApp.Models;

namespace WebApp.Tests
{
    public class PeriodRosterTests
    {
        private const string AbleBaker = "Able Baker";
        private const string CharlieAble = "Charlie Able";

        [Fact]
        public void GetSortedRoster_ReturnsNamesInCorrectFormat()
        {
            var periodRoster = new PeriodRoster { Period = 1, StudentNames = new string[] { AbleBaker } };
            var sortedNames = periodRoster.GetSortedRoster();

            Assert.Equal(AbleBaker, sortedNames[0]);

        }

        [Theory]
        [InlineData(new string[] { AbleBaker, CharlieAble }, AbleBaker)]
        [InlineData(new string[] { CharlieAble, AbleBaker }, AbleBaker)]
        public void GetSortedRoster_ReturnsNamesSortedByLastNameDescending(string[] names, string expectedWinner)
        {
            var periodRoster = new PeriodRoster { Period = 1, StudentNames = names };
            var sortedNames = periodRoster.GetSortedRoster();

            Assert.Equal(expectedWinner, sortedNames.First());
        }
    }
}