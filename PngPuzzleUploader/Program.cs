using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PngPuzzleUploader
{
    class Program
    {
        static void NonQueryCommand(string cmdStr)
        {
            string connStr =
            "Server=203.254.143.200;" +
            "Port=18013;" +
            "Database=nonogram;" +
            "Uid=nonouser;" +
            "Pwd=longlivethenonodfs;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                MySqlCommand sqlComm = new MySqlCommand(cmdStr, conn);
                sqlComm.ExecuteNonQuery();
            }
        }

        static void Main(string[] args)
        {
            Console.Write("id : ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("name : ");
            string name = Console.ReadLine();

            string pngPath = @"puzzle.png";

            Console.WriteLine($"Reading {pngPath}...");

            string raw = string.Empty;
            Bitmap bitmap = new Bitmap(pngPath);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    raw += (Color.Black == pixelColor) ? "1" : "0";
                }
            }

            string cmdStr = "INSERT OR REPLACE INTO PuzzleAnswer(ID, Name, Height, Width, PuzzleRawString) " +
                    $"VALUES ({id}, '{name}', {bitmap.Height}, {bitmap.Width}, '{raw}');";

            try
            {
                NonQueryCommand(cmdStr);
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is wrong...");
                Console.WriteLine(e.GetType() + "\n" + e.Message);
            }

        }
    }
}
