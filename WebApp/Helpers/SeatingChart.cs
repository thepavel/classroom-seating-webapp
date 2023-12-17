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
        private string[,] Chart;
        public SeatingChart(ClassPeriod classPeriod)
        {
            _classPeriod = classPeriod;
            Chart = CreateSeatingChart();
        }

        private string[,] CreateSeatingChart()
        {
            var chart = new string[_classPeriod.Rows, _classPeriod.Columns];
            
            return chart;
        }
    }
}