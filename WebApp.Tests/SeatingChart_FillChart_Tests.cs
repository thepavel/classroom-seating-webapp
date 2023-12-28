using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChart_FillChart_Tests
{
    private SeatingChart DefaultFourByFourSeatingChart;
    private StudentName[] DefaultSortedStudents = new StudentName[] {
                                            new("first", "student"),
                                            new("test", "pupil"),
                                            new("otro", "estudiante") };

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
        var seatingChart = new SeatingChart(4, 4, new string[4, 4], DefaultSortedStudents.ToList(), false);

        //when
        var firstStudent = seatingChart.Chart[0, 0];

        //then
        firstStudent.ShouldNotBe(DefaultSortedStudents[0].FullName);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    public void SeatingChart_FillsChart_WhenFillChart_IsTrue(int row, int col, int index)
    {
        //given
        var seatingChart = new SeatingChart(4, 4, new string[4, 4], DefaultSortedStudents.ToList(), true);

        //when
        var firstStudent = seatingChart.Chart[row, col];

        //then
        firstStudent.ShouldBe(DefaultSortedStudents[index].FullName);
    }
}
