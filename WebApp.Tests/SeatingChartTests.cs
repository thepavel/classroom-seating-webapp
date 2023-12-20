using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChartTests
{
    private SeatingChart SeatingChart;
    private static SeatingChart TwoByTwoSeatingChart => new(new ClassPeriod(2,2));

    public SeatingChartTests()
    {
        SeatingChart = new SeatingChart(new ClassPeriod(1, 1));
    }

    [Fact]
    public void GetFirstAvailableInsertLocation_ReturnsFirstXLocation()
    {
        //given
        var seatingChart = SeatingChart;

        //when
        (int row, int column) = SeatingChart.GetFirstAvailableInsertLocation(seatingChart.Chart, 1, 1);

        //then
        row.ShouldBe(0);
        column.ShouldBe(0);
    }

    [Fact]
    public void SeatingChart_IsNotFullInitially()
    {
        //given
        var seatingChart = SeatingChart;

        //when
        var full = seatingChart.HasEmptySpot();

        //then
        full.ShouldBeTrue();
    }

    [Fact]
    public void WhenSeatingChartIsFull_HasEmptySpot_ReturnsFalse()
    {
        //given
        var seatingChart = SeatingChart;
        seatingChart.Chart[0,0] = "asdf";

        //when
        var full = seatingChart.HasEmptySpot();

        //then
        full.ShouldBeFalse();
    }

    [Fact]
    public void WhenChartIsFull_InsertLocationIs00()
    {
        //given
        var seatingChart = SeatingChart;
        seatingChart.Chart[0,0] = "asdf";

        //when
        (int row, int column) = SeatingChart.GetFirstAvailableInsertLocation(seatingChart.Chart, 1, 1);

        //then
        row.ShouldBe(0);
        column.ShouldBe(0);
    }
}
