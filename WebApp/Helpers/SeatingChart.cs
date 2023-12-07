namespace WebApp.Helpers
{
    public class SeatingChart
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public string[,] Chart { get; private set; }
        public List<StudentName> Students { get; private set; }

        //public StudentName[,] StudentSeatingChart { get; private set; }

        public string[,] GetStudentSeatingChart()
        {
            // var row = Enumerable.Repeat("x", Columns).ToArray();
            return CreateDefaultSeatingChart();
        }

        private string[,] CreateDefaultSeatingChart()
        {

            const string defaultValue = "x";
            var chart = new string[Columns, Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    chart[i, j] = defaultValue;
                }
            }

            return chart;
        }

        public SeatingChart(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            //fill with x's initially. seating chart takes StudentNames and outputs strings. start with 'x'
            Chart = GetStudentSeatingChart();
            Students = new List<StudentName>();
        }

    }


}