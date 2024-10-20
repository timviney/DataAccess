using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Structures;

namespace SudokuProblems
{
    [Serializable]
    internal class SudokuProblemsRequest(Difficulty? difficulty) : IRequestObject
    {
        // For deserialization
        public SudokuProblemsRequest() : this(null) { }

        public Difficulty? Difficulty { get; set; } = difficulty;
    }
}
