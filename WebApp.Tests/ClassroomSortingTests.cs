using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Tests;

public class ClassroomSortingTests
{
    private const string AlphaBravo = "Alpha Bravo";
    private const string BravoAlpha = "Bravo Alpha";
    private const string CharlieBravo = "Charlie Bravo";
    private const string AlphaCharlie = "Alpha Charlie";
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
        var sortedRoster = RosterSorter.GetSortedStudentNames(EmptyPeriodRoster.StudentNames);
        Assert.Empty(sortedRoster);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlpha()
    {
        var sortedRoster = RosterSorter.GetSortedStudentNames(TwoStudentsOneClassAB);
        Assert.Equal(AlphaBravo, sortedRoster[0].FullName);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlphaEvenIfSecond()
    {
        // Given
        var studentNames = new string[] { BravoAlpha, AlphaBravo };

        // When
        var sortedRoster = RosterSorter.GetSortedStudentNames(studentNames);

        // Then
        Assert.Equal(AlphaBravo, sortedRoster[0].FullName);
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
    [InlineData(new string[] { CharlieBravo, BravoAlpha }, CharlieBravo)]
    [InlineData(new string[] { BravoAlpha, CharlieBravo }, CharlieBravo)]
    public void StudentRosterSortingFirstItemTests(string[] names, string expectedWinner)
    {
        //given 
        var roster = CreateTestPeriodRoster(names);

        //when
        var sortedRoster = RosterSorter.GetSortedStudentNames(names);

        Assert.Equal(expectedWinner, sortedRoster[0].FullName);
    }


    [Theory]
    [InlineData(new string[] { BravoAlpha, AlphaBravo }, "Bravo")]
    [InlineData(new string[] { CharlieBravo, BravoAlpha }, "Bravo")]
    [InlineData(new string[] { BravoAlpha, CharlieBravo }, "Bravo")]
    public void StudentRosterSorting_FirstElementHasLastAlphabeticallySortedLastName(string[] names, string firstLastName) 
    {
        //given 
        var roster = CreateTestPeriodRoster(names);

        //when
        var sortedNames = RosterSorter.GetSortedStudentNames(roster.StudentNames);

        //then
        Assert.Equal(firstLastName, sortedNames[0].LastName);
    }
}