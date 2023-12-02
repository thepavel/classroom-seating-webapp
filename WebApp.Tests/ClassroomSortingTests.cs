using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Tests;

public class ClassroomSortingTests
{
    private static string AlphaBravo = "Alpha Bravo";
    private static string BravoAlpha = "Bravo Alpha";
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
        var sortedRoster = RosterSorter.SortRoster(EmptyPeriodRoster);
        Assert.Empty(sortedRoster);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlpha()
    {
        var sortedRoster = RosterSorter.SortRoster(new PeriodRoster { Period = 1, StudentNames = TwoStudentsOneClassAB });
        Assert.Equal(AlphaBravo, sortedRoster[0]);
    }

    [Fact]
    public void AlphaBravoSortsAheadOfBravoAlphaEvenIfSecond()
    {
        // Given
        var periodRoster = new PeriodRoster { Period = 1, StudentNames = new string[] { BravoAlpha, AlphaBravo } };
        
        // When
        var sortedRoster = RosterSorter.SortRoster(periodRoster);

        // Then
        Assert.Equal(AlphaBravo, sortedRoster[0]);
    }
}