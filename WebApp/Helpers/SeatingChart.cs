namespace WebApp.Helpers
{
    public class SeatingChart
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public List<StudentName>[] Chart { get; private set; }

        public SeatingChart(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Chart = new List<StudentName>[columns];

            for (var i = 0; i < rows; i++) {
                Chart[i] = new List<StudentName>(capacity: rows);
            }
            
        }
    }


}