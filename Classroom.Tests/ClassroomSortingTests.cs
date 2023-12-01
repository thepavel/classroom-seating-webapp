namespace Classroom.Tests;
using WebApp.Models;
using WebApp.Helpers;

public class ClassroomSortingTests
{
    private PeriodRoster EmptyPeriodRoster = new PeriodRoster { Period = 1, StudentNames = Array.Empty<string>() };

    [Fact]
    public void CanCreateClassroom()
    {
        var model = new ClassroomModel();
        model.Columns = 1;
        Assert.True(model.Columns == 1);
        //Assert.True(false, "fuck");
    }

    [Fact]
    public void RosterSorterReturnsEmptyArrayWhenStudenNamesIsEmpty() {
        var sortedRoster = RosterSorter.SortRoster(EmptyPeriodRoster);
        Assert.Empty(sortedRoster);
    }

}