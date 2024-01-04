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

    

}
