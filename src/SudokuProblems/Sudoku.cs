using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuProblems
{
    internal record Sudoku(int[][] Grid)
    {
        private const int DefaultSize = 9;

        public Sudoku() : this((int[][])null){}
        public Sudoku(string puzzleString) : this(new int[DefaultSize][])
        {
            int row = -1;
            int col = -1;
            var size = DefaultSize*DefaultSize;
            for (int i = 0; i < size; i++)
            {
                if (i % DefaultSize == 0)
                {
                    row++;
                    col = 0;
                    Grid[row] = new int[DefaultSize];
                }

                Grid[row][col] = int.Parse(puzzleString[i..(i+1)]);
                col++;
            }
        }
    }
}
