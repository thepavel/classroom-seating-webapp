using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Tests;

public class ClassroomSortingTests
{
    private const string AlphaBravo = "Alpha Bravo";
    private const string BravoAlpha = "Bravo Alpha";
    private const string CharlieBravo = "Charlie Bravo";
    private const string AlphaCharlie = "Alpha Charlie";

    [Fact]
    public void CanCreateClassroom()
    {
        var model = new ClassroomModel();
        Assert.NotNull(model);
    }

    [Fact]
    public void EmptyListsComeBackEmpty()
    {
        var sortedRoster = RosterSorter.GetSortedStudentNames(Array.Empty<string>());
        Assert.Empty(sortedRoster);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlpha()
    {
        var sortedRoster = RosterSorter.GetSortedStudentNames(new string[] { AlphaBravo, BravoAlpha });
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

    private static PeriodRoster CreateTestPeriodRoster(string[] names)
    {
        return new PeriodRoster { Period = 1, StudentNames = names };
    }

    [Theory]
    [InlineData(new string[] { BravoAlpha, AlphaBravo }, AlphaBravo)]
    [InlineData(new string[] { CharlieBravo, BravoAlpha }, CharlieBravo)]
    [InlineData(new string[] { BravoAlpha, CharlieBravo }, CharlieBravo)]
    [InlineData(new string[] { BravoAlpha, AlphaCharlie }, AlphaCharlie)]
    public void StudentRosterSortingFirstItemTests(string[] names, string expectedWinner)
    {
        //given  [names]
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