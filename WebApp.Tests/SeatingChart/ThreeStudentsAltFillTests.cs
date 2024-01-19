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

    public static IEnumerable<object[]> SecondStudent_TestData => new List<object[]> {
        new object[] { 3, 1, 1, 0 }, // second student should be in 1st spot in 2nd row [1,0]
        new object[] { 1, 3, 0, 1 }, // second student should be in middle spot in 1st row [0,1]
        new object[] { 2, 2, 0, 1 }, // second student should be in 1st row [0,1]

    };

    public List<StudentName> Students { get; }

    [Theory]
    [MemberData(nameof(SecondStudent_TestData))]
    public void SecondStudentIsPlacedIn_CorrectPlace(int numRows, int numColumns, int row, int column)
    {
        //given
        var seatingChart = new SeatingChart(numRows, numColumns, Students, true);

        //when
        var student = Students[1].FullName;

        //then
        seatingChart.Chart[row, column].ShouldBe(student);
    }

    [Theory]
    [InlineData(2, 2, 1, 0)] //a 2x2 classroom should put 3rd student in 2nd row [1,0]
    [InlineData(1, 3, 0, 2)] //a 1x3 classroom should put 3rd student in 3rd spot in 1st row [0,2]
    [InlineData(3, 1, 2, 0)] //a 3x1 classroom should put 3rd student into 3rd row [2,0]
    [InlineData(2, 3, 1, 1)] //a 2x3 classroom should put 3rd student in the back row between the first two students [1,1]
    [InlineData(3, 2, 2, 0)] //a 3x2 classroom should put 3rd student in the back row [2,0]
    [InlineData(2, 4, 1, 1)] //a 2x4 classroom should put 3rd student in 2nd row [1,1]
    public void ThirdStudentIsPlacedIn_CorrectPlace(int numRows, int numColumns, int row, int column)
    {
        //given
        var seatingChart = new SeatingChart(numRows, numColumns, Students, true);

        //when
        var thirdStudent = Students[2].FullName;

        //then
        seatingChart.Chart[row, column].ShouldBe(thirdStudent);
    }
}
