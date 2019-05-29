using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace CloudDBServerTestProject
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

        string connStr =
            "Server=203.254.143.200;" +
            "Port=18013;" +
            "Database=test;" +
            "Uid=nonouser;" +
            "Pwd=longlivethenonodfs;";

        private void LoadDBFromServer(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "select * from test_table;";
                // DataAdapter 는 데이터를 받아오고 연결을 끊는다.
                // 연결을 유지하고 싶다면 DataReader 를 쓰자.
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dataTable);

                conn.Close();
            }

            myDataGrid.DataContext = dataTable;
        }

        private void SaveDataToDBServer(object sender, RoutedEventArgs e)
        {
            string user = userTextBox.Text;
            string msg = messageTextBox.Text;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string sqlStr = string.Format($"insert into test_table(user, message) values('{user}', '{msg}');");
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                LoadDBFromServer(sender, e);

                conn.Close();
            }
        }
    }
}
