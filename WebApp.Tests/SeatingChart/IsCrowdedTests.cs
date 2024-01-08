using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class IsCrowdedTests
{
    public SeatingChart Default_OneXOne_SeatingChart = new(1, 1, new List<StudentName>());

    [Fact]
    public void Default_OneXOne_Cell_IsNeverCrowded() 
    {
        //given a ONE x ONE classroom, so no more students can be added
        var chart = new SeatingChart(1, 1, new List<StudentName>()).Chart;

        //when
        var isCrowded = SeatingChart.IsCrowded(chart, 0, 0);

        //then
        isCrowded.ShouldBeFalse();
    }


    [Fact]
    public void SingleSeatInClassroom_WhenFilled_IsCrowdedReturnsFalse()
    {
        //given a ONE x ONE classroom, so no more students can be added
        var student = new StudentName("some", "student");
        var chart = new SeatingChart(1, 1, new List<StudentName>(){student}).Chart;

        //when
        var isFilled = SeatingChart.SeatIsFilled(chart, 0, 0);
        var isCrowded = SeatingChart.IsCrowded(chart, 0, 0);

        //then
        isFilled.ShouldBeTrue();
        isCrowded.ShouldBeFalse();
    }

}
