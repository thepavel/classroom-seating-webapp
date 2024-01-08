using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class IsFilledTests
{

    [Fact]
    public void Empty_Students_Means_SeatNotFilled()
    {
        //given
        var chart = new SeatingChart(1, 1, new List<StudentName>()).Chart;

        //when
        var isFilled = SeatingChart.SeatIsFilled(chart, 0, 0);

        //then
        isFilled.ShouldBeFalse();
    }

    [Fact]
    public void EmptySeats_HaveAnX() 
    {
        //given
        var chart = new SeatingChart(1, 1, new List<StudentName>()).Chart;

        //when
        var name = chart[0,0];

        //then
        name.ShouldBe("x");
    }

    [Fact]
    public void SeatingChart_CanCreate_FilledSeats()
    {
        //given ONExONE chart with one student in list
        var students = new List<StudentName>(){ new("some", "student")};
        var chart = new SeatingChart(1, 1, students).Chart;

        //when
        var isFilled = SeatingChart.SeatIsFilled(chart, 0, 0);

        //then
        isFilled.ShouldBeTrue();
    }

    [Fact]
    public void Seat_IsFilled_WhenNotX() 
    {
        //given 
        var chart = new SeatingChart(1, 1, new List<StudentName>()).Chart;
        
        //when
        chart[0,0] = "y";

        //then
        SeatingChart.SeatIsFilled(chart, 0, 0).ShouldBeTrue();


    }
}
