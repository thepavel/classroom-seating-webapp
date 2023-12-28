using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChart_FillChart_Tests
{
    private SeatingChart DefaultFourByFourSeatingChart;
    private readonly StudentName[] Students = new StudentName[] {
                                            new("first", "student"),
                                            new("test", "pupil"),
                                            new("some", "kid"),
                                            new("otro", "estudiante"),
                                            new("wild", "child")
                                             };

    public ClassPeriod DefaultFourByFourClassPeriod { get; }

    public SeatingChart_FillChart_Tests()
    {
        DefaultFourByFourSeatingChart = new SeatingChart(new ClassPeriod(4, 4));
        DefaultFourByFourClassPeriod = new ClassPeriod(4, 4);
    }

    [Fact]
    public void SeatingChart_DoesNotFillChart_WhenFillChartSetToFalse()
    {
        //given
        var seatingChart = new SeatingChart(4, 4, new string[4, 4], Students.ToList(), false);

        //when
        var firstStudent = seatingChart.Chart[0, 0];

        //then
        firstStudent.ShouldNotBe(Students[0].FullName);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 0, 4)]
    public void SeatingChart_FillsChart_WhenFillChart_FromFrontToBack(int row, int col, int index)
    {
        //given
        var seatingChart = new SeatingChart(4, 4, new string[4, 4], Students.ToList(), true);

        //when
        var firstStudent = seatingChart.Chart[row, col];

        //then
        firstStudent.ShouldBe(Students[index].FullName);
    }
}
