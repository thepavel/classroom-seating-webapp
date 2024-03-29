﻿using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChart_FillChart_Tests
{
    private SeatingChart DefaultFourByFourSeatingChart;

    public SeatingChart AltFill_SeatingChart { get; }

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
        AltFill_SeatingChart = new SeatingChart(4, 4, new List<StudentName>(Students), useAlternateFill: true);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 0, 4)]
    public void SeatingChart_FillsChart_FromFrontToBack(int row, int col, int index)
    {
        //given
        var seatingChart = DefaultFourByFourSeatingChart;

        //when
        var studentName = seatingChart.Chart[row, col];

        //then
        studentName.ShouldBe(Students[index].FullName);
    }



    [Theory]
    [InlineData(5, 1, 2)] //sixth student is at the end of 2nd row
    [InlineData(4, 1, 1)] //fifth student is in middle of 2nd row
    [InlineData(5, 2, 1, true)] //sixth student is in middle of 3rd row with alt-fill
    [InlineData(4, 1, 2, true)] //fifth student is at end of 2nd row with alt-fill
    public void AltFill_3x3_SixStudents(int studentIndex, int row, int col, bool altFill = false)
    {
        //given
        var students = new List<StudentName>(Students)
        {
            new("some", "bastard")
        };

        var seatingChart = new SeatingChart(3, 3, students, altFill);

        //when
        var studentName = students[studentIndex].FullName;

        //then
        seatingChart.Chart[row, col].ShouldBe(studentName);
    }

    [Fact]
    public void AlternateFill_SeatsFirstStudentCorrectly()
    {
        //given
        var seatingChart = AltFill_SeatingChart;

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
