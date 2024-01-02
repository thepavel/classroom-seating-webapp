
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
        public int Rows { get; }
        public int Columns { get; }
        public string[,] Chart { get; private set; }

        private const string EmptySpaceSymbol = "x";
        private readonly List<StudentName> Students;

        public SeatingChart(ClassPeriod classPeriod) 
        : this(classPeriod.Rows, classPeriod.Columns, classPeriod.Students)
        {
            
        }

        public SeatingChart(int rows, int columns, List<StudentName> students, bool fillChart = false)
        {
            Rows = rows;
            Columns = columns;
            Chart = CreateDefaultSeatingChart(rows, columns);
            Students = students;

            if(fillChart) {
                FillChartWithStudents();
            }
        }

        private static string[,] CreateDefaultSeatingChart(int rows, int columns)
        {
            //fill with x's initially
            //TODO: make this a view responsibility

            var chart = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    chart[i, j] = EmptySpaceSymbol;
                }
            }

            return chart;
        }

        public static SeatingChart CollapseFullSeatingChart(SeatingChart seatingChart)
        {
            //use this method to collapse seating charts.

            return seatingChart;
        }

        public int GetIndexFromRowAndColumn(int row, int column)
        {
            return row * Columns + column;
        }

        private void FillChartWithStudents()
        {
            //TODO: eventually get to a method of "if there's no uncrowded spot, then collapse chart and put it int the spot that's one off from the last filled spot"
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var studentIndex = GetIndexFromRowAndColumn(i, j);
                    if (studentIndex < Students.Count)
                    {
                        Chart[i, j] = Students[studentIndex].FullName;
                    }
                    else 
                    {
                        break;
                    }

                }
            }

            // for (int i = 0; i < Students.Count; i++)
            // {
            //     var studentName = Students[i];
            //     (int row, int col) = GetFirstEmptySeat();
            //     Chart[row, col] = studentName.FullName;
            // }

            // (int row, int column) = GetFirstEmptyUncrowdedSeat();
            // bool everythingCrowded = row < 0 && column < 0;

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
                    return a new seating chart where the first row consists of students[0..column]
                                            and the rest consists of the same approach with a chart minus 1row and students minus those that are filling the first row



                if(noMoreUncrowdedSpots) { // need to figure out how to do this elegantly. there has to be a way. 
                //something can be inferred about the classroom or the remaining names in a way that uses recursion. 
                //there's some function that can take a list of student names and a chart and spit one out that is collapsed
                //it would be like
                    func collapseFirstRow()
                    {
                        //returns a new seating chart object with the first row filled and the rest ... distributed?

                        remainingStudents = students[columns..?] 
                        var newChart = new SeatingChart = new { rows = Rows - 1, columns = Columns, Students = remainingStudents}
                        return some amalgam of the current row that just got collapsed
                        AND the remaining rows.
                        so... 
                        currentRow = 0;
                        for(var i = currentRow; i < columns; i++) {
                            if(students[i])
                                chart[currentRow, i] = students[i]
                        }

                    }




                    studentNamesToFillRow = students.from(row*column..(row+1)*column)
                    chart

                }
            */
            // if (!everythingCrowded)
            // {
            //     foreach (var studentName in Students)
            //     {

            //         if (!IsCrowded(row, column))
            //         {
            //             Chart[row, column] = studentName.FullName;
            //         }
            //         else
            //         {
            //             //keep walking, but ... default to here
            //             /* find first empty spot */
            //             /*
            //              * GIVEN an empty spot AND no uncrowded spots, 
            //              * WHEN determining where to insert, 
            //             */
            //         }

            //     }
            // }
            // else 
            // {
            //     //everything is crowded. 
            //     // if there is 1 empty spot that's not at the end
            //     // we still need to insert at end, so... 
            //     /*
            //     * 1. shift everything to top-left
            //     */
            // }
        }
        public (int, int) GetFirstEmptyUncrowdedSeat()
        {
            //walk each row left to right and find the first spot that isn't filled
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Chart[i, j] == "x" && !IsCrowded(i, j))
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
            //walk each row left to right and find the first spot that isn't filled
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Chart[i, j] == "x")
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
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

        public bool IsSeatLeftFilled(int row, int column)
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

        public bool SeatIsAvailable(int row, int column, int rowOffset = 0, int columnOffset = 0)
        {
            var rowIndex = row + rowOffset;
            var columnIndex = column + columnOffset;

            return SeatIsInbound(rowIndex, columnIndex)
                    && Chart[rowIndex, columnIndex] == "x";
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

    }
}