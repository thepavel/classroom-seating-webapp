
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

    private const string EmptySpaceSymbol = "x";
    private readonly List<StudentName> Students;

    public SeatingChart(int rows, int columns, List<StudentName> students, bool useAlternateFill = false)
    {
        Rows = rows;
        Columns = columns;
        Chart = CreateDefaultSeatingChart(rows, columns);
        Students = students;
        OpenSeat = new Tuple<int, int>(0, 0);

        FillChartWithStudents();

        if (useAlternateFill)
        {
            Chart = DistributeStudents(Chart, Students);
        }

    }

    private string[,] DistributeStudents(string[,] chart, List<StudentName> students)
    {

        var newChart = CreateDefaultSeatingChart(Rows, Columns);


        for (int i = 0; i < Students.Count; i++)
        {
            var studentName = Students[i];
            (int row, int col) = GetFirstEmptySeat();

            var isCrowded = IsCrowded(chart, row, col);

            if (isCrowded)
            {

                //get next available spot and update row/col if needed
                (int newRow, int newCol) = GetFirstEmptyUncrowdedSeat(newChart);

                if (newRow != -1 && newCol != -1)
                {
                    //there's an empty uncrowded seat
                    
                    row = newRow;
                    col = newCol;
                }
                else {
                    //there are no more empty uncrowded seats
                    //TODO, collapse chart and try again with a smaller chart subset
                }

            }

            chart[row, col] = studentName.FullName;
        }

        // (int row, int column) = GetFirstEmptyUncrowdedSeat();
        // bool everythingCrowded = row < 0 && column < 0;

        return chart;


        /*
        * alternate approach to filling front before back:
        * when no more uncrowded spots, collapse everything
        * to fill first row. distribute everyone else in the remaining rows.
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
                    and the rest consists of the same approach with a chart minus 1 row and remaining students
        */

    }

    private static string[,] CreateDefaultSeatingChart(int rows, int columns)
    {
        //TODO: make this a view responsibility

        var chart = new string[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                chart[i, j] = EmptySpaceSymbol;

        return chart;
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

    private void FillChartWithStudents()
    {
        string[,] chart = CreateDefaultSeatingChart(Rows, Columns);
        Chart = FillChartWithStudents(chart);

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

    public (int, int) GetFirstEmptyUncrowdedSeat(string[,] chart)
    {
        var rows = chart.GetLength(0);
        var columns = chart.GetLength(1);

        //walk each row left to right and find the first spot that isn't filled
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (chart[i, j] == "x" && !IsCrowded(chart, i, j))
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }
    public bool IsSeatEmpty(int row, int column)
    {
        bool empty = Chart[row, column] == "x";
        return empty &&
                (Students.Count == 0 || (row > 0 && column > 0));

        //walk each row left to right and find the first spot that isn't filled
    }

    public (int, int) GetFirstEmptySeat()
    {
        (int row, int col) = GetFirstEmptySeat(Chart, Rows, Columns);
        OpenSeat = new Tuple<int, int>(row, col);

        return (row, col);
    }

    private static (int, int) GetFirstEmptySeat(string[,] chart, int rows, int columns)
    {
        //TODO: possibly move to a SeatingChartFiller class or module?
        //walk each row left to right and find the first spot that isn't filled
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (chart[i, j] == "x")
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
                if (Chart[i, j] == "x")
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

    public static bool IsSeatRightFilled(string[,] chart, int row, int col)
    {
        //right = col + 1
        return SeatIsFilled(chart, row, col, columnOffset: 1);
    }

    public static bool IsSeatLeftFilled(string[,] chart, int row, int column)
    {
        //left = column - 1
        return SeatIsFilled(chart, row, column, columnOffset: -1);
    }

    public static bool IsSeatBehindFilled(string[,] chart, int rowIndex, int columnIndex)
    {
        //back = row + 1 
        return SeatIsFilled(chart, rowIndex, columnIndex, rowOffset: 1);
    }

    public static bool IsSeatAheadFilled(string[,] chart, int row, int column)
    {
        //ahead = row - 1
        return SeatIsFilled(chart, row, column, rowOffset: -1);
    }

    public static bool SeatIsInboundAndAvailable(string[,] chart, int row, int column, int rowOffset = 0, int columnOffset = 0)
    {
        var rowIndex = row + rowOffset;
        var columnIndex = column + columnOffset;

        return SeatIsInbound(chart, rowIndex, columnIndex)
                && !SeatIsInboundAndFilled(chart, rowIndex, columnIndex);
    }

    public static bool SeatIsFilled(string[,] chart, int row, int column, int rowOffset = 0, int columnOffset = 0)
    {
        var rowIndex = row + rowOffset;
        var columnIndex = column + columnOffset;

        return SeatIsInboundAndFilled(chart, rowIndex, columnIndex);
    }

    private static bool SeatIsInboundAndFilled(string[,] chart, int row, int col)
    {
        return SeatIsInbound(chart, row, col)
                && chart[row, col] != "x";
    }

    private static bool SeatIsInbound(string[,] chart, int row, int col)
    {
        return row > 0 && row < chart.GetLength(0)
            && col > 0 && col < chart.GetLength(1);
    }

    public bool SeatIsInbound(int row, int column)
    {
        return SeatIsInbound(Chart, row, column);

    }

}