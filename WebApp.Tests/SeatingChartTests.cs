using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChartTests
{
    private SeatingChart DefaultOneByOneSeatingChart;
    private static SeatingChart TwoByTwoSeatingChart => new(new ClassPeriod(2, 2));

    public SeatingChartTests()
    {
        DefaultOneByOneSeatingChart = new SeatingChart(new ClassPeriod(1, 1));
    }

    [Fact]
    public void GetFirstAvailableInsertLocation_ReturnsFirstXLocation()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        (int row, int column) = SeatingChart.GetFirstAvailableInsertLocation(seatingChart.Chart, 1, 1);

        //then
        row.ShouldBe(0);
        column.ShouldBe(0);
    }

    [Fact]
    public void GetFirstAvailableInsertLocation_Returns00_IfFull()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;
        seatingChart.Chart[0, 0] = "asdf";

        //when
        (int row, int column) = SeatingChart.GetFirstAvailableInsertLocation(seatingChart.Chart, 1, 1);

        //then
        seatingChart.HasEmptySpot().ShouldBeFalse();
        row.ShouldBe(0);
        column.ShouldBe(0);
    }

    [Fact]
    public void SeatingChart_IsNotFullInitially()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        var hasEmptySpot = seatingChart.HasEmptySpot();

        //then
        hasEmptySpot.ShouldBeTrue();
    }

    [Fact]
    public void WhenSeatingChartIsFull_HasEmptySpot_ReturnsFalse()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        seatingChart.Chart[0, 0] = "asdf";

        //then
        seatingChart.HasEmptySpot().ShouldBeFalse();
    }

    [Fact]
    public void WhenSeatingChartHasEmptySpotAtTheFront_HasEmptySpot_ReturnsTrue()
    {
        //given 
        var seatingChart = new SeatingChart(new ClassPeriod(1, 2));

        //when
        seatingChart.Chart[0, 0] = "asdf";

        //then
        seatingChart.HasEmptySpot().ShouldBeTrue();
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(1, 2)]
    public void HasEmptySpot_ReturnsTrue_Regardless_OfEmptySpotLocation(int numRows, int numColumns)
    {
        //given
        var seatingChart = new SeatingChart(new ClassPeriod(numRows, numColumns));

        //when
        seatingChart.Chart[0, 0] = "asdf";

        //then
        seatingChart.HasEmptySpot().ShouldBeTrue();
    }

    [Fact]
    public void WhenChartIsFull_InsertLocationIs00()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;
        seatingChart.Chart[0, 0] = "asdf";

        //when
        (int row, int column) = SeatingChart.GetFirstAvailableInsertLocation(seatingChart.Chart, 1, 1);

        //then
        row.ShouldBe(0);
        column.ShouldBe(0);
    }

    //not ready yet. returns false.
    internal void EmptyChartHasUncrowdedSpot()
    {
        //given 
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        bool hasUncrowdedSpot = seatingChart.IsCrowded(0, 0);

        //then
        hasUncrowdedSpot.ShouldBeTrue();
    }

    [Fact]
    public void IsRightSeatOpenReturnsTrueWhenRightSeatOpen_AndAllOtherSeatsOutOfBounds()
    {
        //given 
        var classPeriod = new ClassPeriod(1, 2);
        var seatingChart = new SeatingChart(classPeriod);
        (int row, int col) = (0, 0);

        //when
        var open = !seatingChart.IsSeatRightFilled(row, col);

        //then
        open.ShouldBeTrue();

    }

    [Fact]
    public void IsRightSeatFilledReturnsFalseWhenRightSeatOpen()
    {
        //given 
        var classPeriod = new ClassPeriod(3, 3);
        var seatingChart = new SeatingChart(classPeriod);
        (int row, int col) = (1, 1);

        //when
        var isRightSeatOpen = !seatingChart.IsSeatRightFilled(row, col);

        //then
        isRightSeatOpen.ShouldBeTrue();

    }

    [Fact]
    public void IsRightSeatFilled_Empty3x3_AllPositions_ShouldBeFalse()
    {
        //given
        var seatingChart = ThreeByThreeSeatingChart;

        //when
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //then
                seatingChart.IsSeatRightFilled(i, j).ShouldBeFalse();
            }
        }

    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, false)]
    [InlineData(-1, 0, false)]
    [InlineData(0, -1, false)]
    public void SeatIsInbound_IsTrue_WhenSeatIsWithinTheChartRange(int row, int column, bool expectedResult)
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        var inbound = seatingChart.SeatIsInbound(row, column);

        //then
        expectedResult.ShouldBe(inbound);
    }

    public static SeatingChart ThreeByThreeSeatingChart => new SeatingChart(new ClassPeriod(3, 3));

}
