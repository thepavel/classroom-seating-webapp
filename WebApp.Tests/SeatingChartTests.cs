using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChartTests
{
    private readonly SeatingChart DefaultOneByOneSeatingChart;
    private static SeatingChart TwoByTwoSeatingChart => new(2, 2, new List<StudentName>());

    public SeatingChartTests()
    {
        DefaultOneByOneSeatingChart = new SeatingChart(1, 1, new List<StudentName>());
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void GetFirstEmptySeat_Returns00_ForDefaultSeatingChart(int rows, int columns)
    {
        //given
        var seatingChart = new SeatingChart(rows, columns, new List<StudentName>());

        //when
        (int row, int column) = seatingChart.GetFirstEmptySeat();

        //then
        row.ShouldBe(0);
        column.ShouldBe(0);
    }

    [Theory]
    [InlineData(0,0,0)]
    [InlineData(1,0,4)]
    public void GetIndexFromRowAndColumn_ReturnsArrayIndexCorrectly(int row, int column, int expectedResult) 
    {
        //given 
        var seatingChart = new SeatingChart(4, 4, new List<StudentName>());

        //when
        var index = seatingChart.GetIndexFromRowAndColumn(row, column);

        //then
        expectedResult.ShouldBe(index);
    }

    [Fact]
    public void GetFirstAvailableInsertLocation_ReturnsInvalidValues_IfFull()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;
        seatingChart.Chart[0, 0] = "asdf";

        //when
        (int row, int column) = seatingChart.GetFirstEmptySeat();

        //then
        seatingChart.HasEmptySpot().ShouldBeFalse();
        row.ShouldBe(-1);
        column.ShouldBe(-1);
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
        var seatingChart = new SeatingChart(1, 2, new List<StudentName>());

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
        var seatingChart = new SeatingChart(numRows, numColumns, new List<StudentName>());

        //when
        seatingChart.Chart[0, 0] = "asdf";

        //then
        seatingChart.HasEmptySpot().ShouldBeTrue();
    }

    [Fact]
    public void WhenChartIsFull_GetFirstEmptySeat_Is_Invalid()
    {
        //given
        var seatingChart = DefaultOneByOneSeatingChart;
        seatingChart.Chart[0, 0] = "asdf";

        //when
        (int row, int column) = seatingChart.GetFirstEmptySeat();

        //then
        row.ShouldBe(-1);
        column.ShouldBe(-1);
    }

    //not ready yet. returns false.
    [Fact]
    public void EmptyOneByOneChart_HasUncrowdedSpot_At_0_0()
    {
        //given 
        var seatingChart = DefaultOneByOneSeatingChart;

        //when
        bool spotIsCrowded = seatingChart.IsCrowded(0, 0);

        //then
        spotIsCrowded.ShouldBeFalse();
    }

    [Fact]
    public void FilledOneByOneChart_StillHasUncrowdedSpot_At_0_0()
    {
        //given 
        var seatingChart = DefaultOneByOneSeatingChart;
        seatingChart.Chart[0, 0] = "asdf";

        //when
        bool isFilled = seatingChart.SeatIsFilled(0, 0);
        bool spotIsCrowded = seatingChart.IsCrowded(0, 0);

        //then
        isFilled.ShouldBeTrue();
        spotIsCrowded.ShouldBeFalse();
    }

    [Fact]
    public void IsRightSeatOpenReturnsTrueWhenRightSeatOpen_AndAllOtherSeatsOutOfBounds()
    {
        //given 
        var seatingChart = new SeatingChart(1, 2, new List<StudentName>());
        (int row, int col) = (0, 0);

        //when
        var open = !seatingChart.IsSeatRightFilled(row, col);

        //then
        open.ShouldBeTrue();

    }

    [Theory]
    [InlineData(1, 1, false)]
    [InlineData(1, 2, false)]
    public void IsSeatRightFilled_ReturnsFalse_If_EmptyOrAtRightEdge(int rows, int columns, bool expectedResult)
    {
        //given
        var seatingChart = new SeatingChart(rows, columns, new List<StudentName>());

        //when
        (int row, int col) = (0, 0);

        //then
        seatingChart.IsSeatRightFilled(0, 0).ShouldBe(expectedResult);
    }

    [Fact]
    public void IsRightSeatFilledReturnsFalseWhenRightSeatOpen()
    {
        //given 
        var seatingChart = new SeatingChart(3, 3, new List<StudentName>());
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

    public static SeatingChart ThreeByThreeSeatingChart => new SeatingChart(3, 3, new List<StudentName>());

}
