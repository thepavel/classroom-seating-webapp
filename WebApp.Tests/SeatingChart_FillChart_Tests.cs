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


    public SeatingChart_FillChart_Tests()
    {
        DefaultFourByFourSeatingChart = new SeatingChart(4, 4, new List<StudentName>(Students));
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 0, 4)]
    public void SeatingChart_FillsChart_FromFrontToBack(int row, int col, int index)
    {
        //given
        var seatingChart = new SeatingChart(4, 4, Students.ToList());

        //when
        var firstStudent = seatingChart.Chart[row, col];

        //then
        firstStudent.ShouldBe(Students[index].FullName);
    }

    [Fact]
    public void AlternateFill_SeatsFirstStudentCorrectly()
    {
        //given
        var seatingChart = new SeatingChart(4, 4, Students.ToList(), useAlternateFill: true);

        //when
        var firstStudent = seatingChart.Chart[0, 0];

        //then
        firstStudent.ShouldBe(Students[0].FullName);
    }

    [Fact]
    public void AlternateFill_DoesNotOverfill_1x1_Chart()
    {
        //given 
        var seatingChart = new SeatingChart(1, 1, Students.ToList(), useAlternateFill: true);

        //when
        var firstStudent = seatingChart.Chart[0, 0];

        //then
        firstStudent.ShouldBe(Students[0].FullName);

    }

}
