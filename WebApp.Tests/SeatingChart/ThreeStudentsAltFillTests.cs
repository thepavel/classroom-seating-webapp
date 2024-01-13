using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class ThreeStudentsAltFillTests
{
    public ThreeStudentsAltFillTests()
    {
        Students = new List<StudentName> {
            new("first", "student"),
            new("next", "pupil"),
            new("third", "one")
        };
    }

    public List<StudentName> Students { get; }

    [Fact]
    public void ThirdStudentIsPlacedIn_CorrectPlace()
    {
        //given
        var seatingChart = new SeatingChart(2, 3, Students, true);

        //when
        var thirdStudent = Students[2].FullName;

        //then
        seatingChart.Chart[1,1].ShouldBe(thirdStudent);
    }
}
