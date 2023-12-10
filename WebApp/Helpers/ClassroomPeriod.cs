using WebApp.Models;

namespace WebApp.Helpers
{
    /// <summary>
    /// Class keeps a list of students and returns a seating chart based on available seats, and following seating preferences.
    /// 
    /// current preferences:
    ///     	- Children should be sorted by last name, descending, start with the top left seat, horizontally. 
    ///        	- If there are empty spaces, children should have empty seats between them so as there is not a student next to another horizontally or vertically.
    ///         - Front should fill up before back. 
    /// Implemented via Chart property, which returns current Students seated in an optimal way.
    /// </summary>
    public class ClassroomPeriod
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int Size => Rows * Columns;

        private string[,] Chart { get; set; }
        public List<StudentName> Students { get; private set; }

        private static string[,] CreateDefaultSeatingChart(int rows, int columns)
        {
            //fill with x's initially. seating chart takes StudentNames and outputs strings. start with 'x'

            var chart = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < columns; j++)
                {
                    chart[i, j] = "x";
                }
            }

            return chart;
        }

        public ClassroomPeriod(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Chart = CreateDefaultSeatingChart(Rows, Columns);
            Students = new List<StudentName>();
        }


        /// <summary>
        /// returns true if student can be added to class roster based on capacity. 
        /// returns false if student cannot be added
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public bool AddStudent(StudentName studentName)
        {
            if (Students.Count < Size)
            {
                Students.Add(studentName);
                UpdateChart(studentName);

                return true;
            }
            return false;
        }

        private void UpdateChart(StudentName studentName)
        {
            //needs to be updated to reflect expected insert behavior
            (int rows, int columns) = GetInsertPosition();

            Chart[rows, columns] = studentName.FullName;
        }

        private (int rows, int columns) GetInsertPosition()
        {
            
            return (0, 0);
        }

        public string[,] GetClassroomSeatingChart()
        {
            return Chart;
        }
    }


}