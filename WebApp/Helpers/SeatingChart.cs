
namespace WebApp.Helpers;

//Seating chart assumes the students are already sorted? 
// implementation plan: 
// crud:
/* Create: creates a new SeatingChart object with a sorted list of students
 * Update: Add => Add a student to the mix. seatingchart doesn't know how to sort. would we want it to add? No. 
 *          maybe SeatingChart should be immutable. You create it with a sorted list and dimensions. 
 *          on update, create another one? against this on a basic efficiency basis. need to test? 
 *                  test performance
                        - hypothesis: recreating object (new SeatingChart(rows, columns, newSortedListOfStudents) 
                                        doesn't take much longer than changing the array values.

 * expected behavior: add a node if nothing adjacent... can we test THAT?
 *  given a seating chart of dimensions Rows*Columns, Insert operation should... 
 *      1. place the inserted item in the lowest spot. 
 *      2. if the lowest spot is taken, then take the next spot over, as long as there's not anyone adjacent?
 *          - this looks like a double shift for 
 *      3. behavior before filling front before back that SHOULD work before the gaps disappear:
            - start at zero
                - if empty, fill with name
            - move left 1, if possible, up if possible. 
            - this feels like making a 1D array into a 2d array [NAME x x x x x] becomes [NAME x NAME x x x] but can shift depending on shape?
            - alternative is to not shift at all, but to keep an insert spot precalculated, but may still need to shift... 
            - can we make a 2D array into a 1D array?  this seems like a key behavior for filling front before back... 
                - can we shift a 2D array? NO... :(
            - if 'x', 
                - check left, right, up, and down for name or out of bounds
                - if left/right/up/down are "empty", ok to insert, 
                - else, note first 'x' and keep walking
 */
public class SeatingChart
{
    private Tuple<int, int> OpenSeat;
    public int Rows { get; }
    public int Columns { get; }
    public string[,] Chart { get; private set; }

    private const string EmptySeatSymbol = "x";
    private readonly List<StudentName> Students;

    public SeatingChart(int rows, int columns, List<StudentName> students, bool useAlternateFill = false)
    {
        Rows = rows;
        Columns = columns;
        
        Students = students;
        OpenSeat = new Tuple<int, int>(0, 0);

        var chart = CreateDefaultSeatingChart(rows, columns);

        Chart = FillChartWithStudents(chart);

        if (useAlternateFill)
        {
            Chart = DistributeStudents(Chart, Students);
        }

    }

    private string[,] DistributeStudents(string[,] chart, List<StudentName> students)
    {
        var numRows = chart.GetLength(0);
        var numColumns = chart.GetLength(1);

        //can't handle this scenario yet - do nothing
        if (numRows * numColumns < students.Count) return chart;

        //all students guaranteed to fit at this point.
        return CreateNewChartWithDistributedStudents(students, numRows, numColumns);

    }

    private string[,] CreateNewChartWithDistributedStudents(List<StudentName> students, int numRows, int numColumns)
    {
        var chart = CreateDefaultSeatingChart(numRows, numColumns);


        for (int i = 0; i < students.Count; i++)
        {
            StudentName student = students[i];
            (int row, int col) = GetFirstEmptyUncrowdedSeat(chart);

            if (row == -1 || col == -1) // if there aren't any
            {

                (row, col) = GetFirstEmptySeat(chart);

                if (row != -1 && col != -1)
                {
                    chart = FillFrontRowAndRedistributeChart(chart, students, i);
                    chart[row, col] = student.FullName;
                }

                //todo: read and implement.. this can be recursive... 
                // takes a chart, students, and returns a chart with first row filled with students and the rest 
                // redistributed as a new chart minus one row.
                //method needs to return a chart that's then added to another chart.
                // what stops the recursion? chart having only one row. in that event, collapse row, add new entry at end, append to the rest.

                /*
                * collapse everything to fill first row. 
                * distribute everyone else in the remaining rows.
                * - this can probably be done by simulating a seating chart with omitted students
                * -- basically, if no more uncrowded spots, 
                    - fill first row by shifting everything to top left
                    - once first row is full, 
                        - take all remaining names (subset of students)
                        - create a new seating chart that's minus one row, fill with remaining names
                        - get its output seating chart and replace remaining rows
                        - do so until .... we're in the last spot

                        given: students[0..10]
                        return a new seating chart where 
                            the first row consists of students[0..columns]
                            and the rest:
                                - apply same sorting approach with remaining students
                                - minus one row for seating chart until it's below 1
                */
            }

            chart[row, col] = student.FullName;

        }

        return chart;
    }

    private static string[,] CreateDefaultSeatingChart(int rows, int columns)
    {
        //TODO: make this a view responsibility

        var chart = new string[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                chart[i, j] = EmptySeatSymbol;

        return chart;
    }

    private static string[,] FillFrontRowAndRedistributeChart(string[,] chart, List<StudentName> students, int studentIndex)
    {
        var rows = chart.GetLength(0);
        var columns = chart.GetLength(1);
        for (var i = 0; i < students.Count; i++)
        {
            if (i < columns)
            {
                chart[0, i] = students[i].FullName;
            }
        }

        var remainingStudents = students.Skip(studentIndex).Take(students.Count - studentIndex).ToList();
        UpdateChartWithRemainingStudents(chart, rows, columns, remainingStudents);

        return chart;
    }

    private static void UpdateChartWithRemainingStudents(string[,] chart, int rows, int columns, List<StudentName> remainingStudents)
    {
        if (rows > 1)
        {
            var subChart = new SeatingChart(rows - 1, columns, remainingStudents, true);
            for (var i = 1; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    chart[i, j] = subChart.Chart[i - 1, j];
                }
            }
        }
    }

    public static SeatingChart CollapseFullSeatingChart(SeatingChart seatingChart)
    {

        //use this method to collapse seating charts.
        //there's some function that can take a list of student names and a chart and spit one out that is collapsed
        //it would be like
        /*  func collapseFirstRow(numRows, numColumns, students)
            {
                //returns a new chart with the first row filled and the rest ... distributed?

                remainingStudents = students[numColumns...] 
                firstRowStudents = students[0..numColumns]
                var firstRowSeatingChart = new SeatingChart(1, columns, firstRowStudents);
                var firstRowChart = firstRowSeatingChart.Chart;
                var newSeatingChart = new SeatingChart(numRows - 1, columns, remainingStudents}

                return some amalgam of the current row that just got collapsed
                AND the remaining rows.


            } */

        return seatingChart;
    }

    private int GetIndexFromRowAndColumn(int row, int column, int columns)
    {
        return row * columns + column;
    }

    private string[,] FillChartWithStudents(string[,] emptyChart)
    {
        var chart = emptyChart;

        //TODO: deal with waitlist or trim students?
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                var studentIndex = GetIndexFromRowAndColumn(i, j, Columns);
                if (studentIndex < Students.Count)
                {
                    chart[i, j] = Students[studentIndex].FullName;
                }
                else
                {
                    break;
                }

            }
        }

        return chart;
    }

    public static (int, int) GetFirstEmptyUncrowdedSeat(string[,] chart)
    {
        var rows = chart.GetLength(0);
        var columns = chart.GetLength(1);

        //walk each row left to right and find the first spot that isn't filled
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (!IsFilled(chart, i, j) && !IsCrowded(chart, i, j))
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    public (int, int) GetFirstEmptySeat()
    {
        (int row, int col) = GetFirstEmptySeat(Chart);
        OpenSeat = new Tuple<int, int>(row, col);

        return (row, col);
    }

    private static (int, int) GetFirstEmptySeat(string[,] chart)
    {
        //TODO: possibly move to a SeatingChartFiller class or module?
        //walk each row left to right and find the first spot that isn't filled

        var rows = chart.GetLength(0);
        var columns = chart.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (chart[i, j] == EmptySeatSymbol)
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }

    public bool HasEmptySpot()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                if (Chart[i, j] == EmptySeatSymbol)
                {
                    return true;
                }
            }
        }

        return false;
    }


    public static bool IsCrowded(string[,] chart, int row, int column)
    {
        return IsSeatAheadFilled(chart, row, column)
                || IsSeatBehindFilled(chart, row, column)
                || IsSeatLeftFilled(chart, row, column)
                || IsSeatRightFilled(chart, row, column);
    }

    public static bool IsSeatRightFilled(string[,] chart, int row, int column)
    {
        //right = col + 1
        var updatedColumn = column + 1;
        return SeatIsInbound(chart, row, updatedColumn)
            && SeatIsFilled(chart, row, updatedColumn);
    }

    public static bool IsSeatLeftFilled(string[,] chart, int row, int column)
    {
        //left = column - 1
        var updatedColumn = column - 1;
        return SeatIsInbound(chart, row, updatedColumn)
            && SeatIsFilled(chart, row, updatedColumn);
    }

    public static bool IsSeatBehindFilled(string[,] chart, int row, int column)
    {
        //back = row + 1 
        int updatedRow = row + 1;
        return SeatIsInbound(chart, updatedRow, column)
            && SeatIsFilled(chart, updatedRow, column);
    }

    public static bool IsSeatAheadFilled(string[,] chart, int row, int column)
    {

        //ahead = row - 1
        int updatedRow = row - 1;

        return SeatIsInbound(chart, updatedRow, column)
                && SeatIsFilled(chart, updatedRow, column);
    }

    public static bool SeatIsFilled(string[,] chart, int row, int column)
    {
        return IsFilled(chart, row, column);
    }
    private static bool IsFilled(string[,] chart, int row, int col) => chart[row, col] != EmptySeatSymbol;

    public static bool SeatIsInbound(string[,] chart, int row, int col)
    {
        return row >= 0 && row < chart.GetLength(0)
            && col >= 0 && col < chart.GetLength(1);
    }

}