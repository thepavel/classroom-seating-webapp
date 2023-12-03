using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Tests;

public class ClassroomSortingTests
{
    private const string AlphaBravo = "Alpha Bravo";
    private const string BravoAlpha = "Bravo Alpha";
    private static PeriodRoster EmptyPeriodRoster => new PeriodRoster { Period = 1, StudentNames = Array.Empty<string>() };
    public static string[] TwoStudentsOneClassAB => new string[] { AlphaBravo, BravoAlpha };

    [Fact]
    public void CanCreateClassroom()
    {
        var model = new ClassroomModel();
        Assert.NotNull(model);
        // model.Columns = 1;
        // Assert.True(model.Columns == 1);
        //Assert.True(false, "fuck");
    }

    [Fact]
    public void TwoStudentsShouldBeSortedByLastName()
    {
        var sortedRoster = RosterSorter.SortStudentNames(EmptyPeriodRoster);
        Assert.Empty(sortedRoster);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlpha()
    {
        var sortedRoster = RosterSorter.SortStudentNames(new PeriodRoster { Period = 1, StudentNames = TwoStudentsOneClassAB });
        Assert.Equal(AlphaBravo, sortedRoster[0]);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlphaEvenIfSecond()
    {
        // Given
        var periodRoster = new PeriodRoster { Period = 1, StudentNames = new string[] { BravoAlpha, AlphaBravo } };

        // When
        var sortedRoster = RosterSorter.SortStudentNames(periodRoster);

        // Then
        Assert.Equal(AlphaBravo, sortedRoster[0]);
    }

    // [Fact]
    // public void CharlieBravoShouldSortBeforeBravoAlpha()
    // {
    //     // Given
    //     var charlieBravo = "Charlie Bravo";
    //     string[] names = new string[] { charlieBravo, BravoAlpha };
    //     // When
    //     var sortedNames = RosterSorter.SortStudentNames(CreateTestPeriodRoster(names));
    //     // Then

    //     Assert.Equal(charlieBravo, sortedNames[0]);
    // }

    private static PeriodRoster CreateTestPeriodRoster(string[] names)
    {
        return new PeriodRoster { Period = 1, StudentNames = names };
    }

    [Theory]
    [InlineData(new string[] { BravoAlpha, AlphaBravo }, AlphaBravo)]
    public void NamesShouldBeOrderedByLastNameDescending(string[] names, string expectedWinner)
    {
        //string[] names = new string[] { BravoAlpha, AlphaBravo };

        var sortedRoster = RosterSorter.SortStudentNames(new PeriodRoster { Period = 1, StudentNames = names });

        Assert.Equal(expectedWinner, sortedRoster[0]);
    }
}