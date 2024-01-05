using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChart_AltFill_TwoStudents_Tests
{
    public SeatingChart_AltFill_TwoStudents_Tests() 
    {
        Students = new List<StudentName> {
            new StudentName("first", "student"),
            new StudentName("next", "pupil")
        };
    }

    public List<StudentName> Students { get; }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SecondStudentIsIgnored_In_1x1_Chart(bool useAltFill) 
    {
        //given 
        var seatingChart = new SeatingChart(1, 1, Students, useAltFill);

        //when
        var student = seatingChart.Chart[0,0];

        //then
        student.ShouldBe(Students[0].FullName);
    }


    [Theory]
    [InlineData(1, 2, 0, 1)] // 1x2 grid should put 2nd student into [0,1]
    [InlineData(2, 1, 1, 0)] // 2x1 grid should put 2nd student into [1,0]
    public void SecondStudentTakesCorrectPlaceInChart(int rows, int columns, int expectedRowIndex, int expectedColumnIndex)
    {
        //given
        var seatingChart = new SeatingChart(rows, columns, Students, useAlternateFill: true);

        //when
        var student = seatingChart.Chart[expectedRowIndex, expectedColumnIndex];

        //then
        student.ShouldBe(Students[1].FullName);
    }


}
