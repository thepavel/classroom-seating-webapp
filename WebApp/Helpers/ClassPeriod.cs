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
    public class ClassPeriod
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int Size => Rows * Columns;

        private string[,] Chart { get; set; }
        public List<StudentName> Students { get; private set; }

        private static string[,] CreateDefaultSeatingChart(int rows, int columns)
        {
            //fill with x's initially
            //TODO: make this a view responsibility

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

        public ClassPeriod(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Chart = CreateDefaultSeatingChart(Rows, Columns);
            Students = new List<StudentName>();
        }

        public ClassPeriod(int rows, int columns, StudentName[] students) :
            this(rows, columns)
        {

            foreach (StudentName studentName in students)
            {
                if (!AddStudent(studentName))
                {
                    break;
                }
            }
            //stops adding students when limit is reached. discards extras
            //todo: add a waitlist
        }


        /// <summary>
        /// returns true if student can be added to class roster based on capacity. 
        /// returns false if student cannot be added
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public bool AddStudent(StudentName studentName)
        {
            var canAddStudent = Students.Count < Size;
            if (Students.Count < Size)
            {

                UpdateChart(studentName);
                Students.Add(studentName);
                
            }
            return canAddStudent;
        }

        private void UpdateChart(StudentName studentName)
        {
            var updatedChart = CreateUpdatedSeatingChart(studentName);
            Chart = updatedChart;
        }

        private string[,] CreateUpdatedSeatingChart(StudentName studentName)
        {

            //TODO: move this to a dedicated class/module

            var seatingChart = Chart;
            //TODO: prepare chart for insert, such as shifting it to insert after full
            (int rows, int columns) = GetInsertPosition();

            seatingChart[rows, columns] = studentName.FullName;
            return seatingChart;
        }

        private (int rows, int columns) GetInsertPosition()
        {
            //handle basic scenario: return the first cell that contains an 'x' and follows an 'x'
            //second basic scenario: return the first 'x' cell in the next row
            // if first 'x' cell in next row is adjacent to a filled cell in row below, shift right or up if possible
            //

            if (Students.Count == 1)
            {
                return (0, 2);
            }
            else
                return (0, 0);
        }

        public string[,] GetClassroomSeatingChart()
        {
            return Chart;
        }
    }
}