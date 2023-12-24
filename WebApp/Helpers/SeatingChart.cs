


namespace WebApp.Helpers
{
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
        private ClassPeriod _classPeriod;

        public int Rows { get; }
        public int Columns { get; }
        public string[,] Chart { get; private set; }
        private readonly List<StudentName> Students;

        public SeatingChart(ClassPeriod classPeriod)
        {
            _classPeriod = classPeriod;
            Rows = classPeriod.Rows;
            Columns = classPeriod.Columns;
            Chart = _classPeriod.GetClassroomSeatingChart();
            Students = _classPeriod.Students;
        }

        public static (int, int) GetFirstAvailableInsertLocation(string[,] chart, int rows, int columns)
        {
            //walk left to right in each row from the front and find the first 'x'
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (chart[i, j] == "x")
                    {
                        return (i, j);
                    }
                }
            }
            return (0, 0);
        }

        private string[,] PopulateSeatingChart(List<StudentName> students)
        {

            //possible solution for adding 
            // var updatedChart = new string[_classPeriod.Rows, _classPeriod.Columns];
            // for(var i = 0; i < _classPeriod.Rows; i++)
            //     for(var j = 0; j < _classPeriod.Columns; j++) 


            foreach (var student in students)
            {
                //always take a new list. never add to an existing structured chart. build the chart anew.
                //foreach student in students : chart.add(student)

            }
            return Chart;
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

        public bool IsCrowded(int row, int column)
        {
            return IsSeatAheadFilled(row, column) 
                    || IsSeatBehindFilled(row, column)
                    || IsSeatLeftFilled(row, column)
                    || IsSeatRightFilled(row, column);
        }

        public bool IsSeatRightFilled(int row, int col)
        {
            //right = col + 1
            return SeatIsFilled(row, col, columnOffset: 1);
        }

        private bool IsSeatLeftFilled(int row, int column)
        {
            //left = column - 1
            return SeatIsFilled(row, column, columnOffset: -1);
        }

        public bool IsSeatBehindFilled(int rowIndex, int columnIndex)
        {
            //back = row + 1 
            return SeatIsFilled(rowIndex, columnIndex, rowOffset: 1);
        }

        public bool IsSeatAheadFilled(int row, int column)
        {
            //ahead = row - 1
            return SeatIsFilled(row, column, rowOffset: -1);
        }


        public bool SeatIsFilled(int row, int column, int rowOffset = 0, int columnOffset = 0)
        {
            var rowIndex = row + rowOffset;
            var columnIndex = column + columnOffset;

            return SeatIsInbound(rowIndex, columnIndex)
                    && Chart[rowIndex, columnIndex] != "x";
        }

        public bool SeatIsInbound(int row, int column)
        {
            return row >= 0 && row < Rows
                    && column >= 0 && column < Columns;
        }

        private static bool SeatIsFilled(string[,] chart, int row, int column)
        {
            return chart[row, column] != "x";
        }

    }
}