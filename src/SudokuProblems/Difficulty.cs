using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuProblems
{
    public enum Difficulty
    {
        // camelCase to align with frontend format and because custom deserialisation is the devil
        easy,
        medium,
        hard
    }
}
