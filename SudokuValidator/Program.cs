using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuValidator
{
    class Program
    {
        static int[,] grid = new int[9,9];
        [STAThread]
        static void Main(string[] args)
        {
            if (LoadGrid())
            {
                PrintArray(grid);
                if (CheckSudokuStatus(grid))
                    Console.WriteLine("Valid Sudoku Solution");
                else
                    Console.WriteLine("Invalid Sudoku Solution");
            }
            Console.ReadLine();
        }

        //Load data from text file into grid
        public static bool LoadGrid()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text Files (txt)|*.txt;";
                ofd.ShowDialog();

                FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);

                using (StreamReader sr = new StreamReader(fs))
                {
                    string aLine;
                    int row = 0;
                    while (true)
                    {
                        int col = 0;
                        aLine = sr.ReadLine();
                        if (aLine == null) break;
                        foreach (char c in aLine)
                        {
                            grid[row, col] = (int)Char.GetNumericValue(c);
                            col++;
                        }
                        if (!String.IsNullOrWhiteSpace(aLine)) row++;

                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid File Format");
                return false;
            }
            
        }

        private static void PrintArray(int[,] grid)
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(grid[i, j].ToString());
                }
                Console.WriteLine();
            }
        }

        private static bool CheckSudokuStatus(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] col = new int[9];
                int[] row = new int[9];
                int[] square = new int[9];

                for (int j = 0; j < 9; j++)
                {
                    row[j] = grid[i, j];
                    col[j] = grid[j,i];
                    square[j] = grid[(i / 3) * 3 + j / 3, i * 3 % 9 + j % 3];
                }
                if (!(Validate(col) && Validate(row) && Validate(square)))
                    return false;
            }
            return true;
        }

        private static bool Validate(int[] val)
        {
            int i = 0;
            Array.Sort(val);
            foreach (int number in val)
                if (number != ++i)
                    return false;
            return true;
        }
    }
}
