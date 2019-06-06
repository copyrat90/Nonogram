using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalDBTestProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=D:\\Project\\C#\\Nonogram\\Nonogram\\LocalDB\\Nonogram.mdf;" +
            "Integrated Security=True";

        private void UpdateDBView()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "select * from PuzzleAnswer;";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dataTable);

                conn.Close();
            }

            dataGrid.DataContext = dataTable;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDBView();
        }

        private void SaveDataToDB(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string height = heightTextBox.Text;
            string width = widthTextBox.Text;
            string puzzleRawString = puzzleRawStringTextBox.Text;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "insert into PuzzleAnswer(Name, Height, Width, PuzzleRawString)" +
                    $"values (N'{name}', {height}, {width}, '{puzzleRawString}');";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            UpdateDBView();
        }
    }
}
