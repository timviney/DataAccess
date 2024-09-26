namespace SudokuProblems
{
    internal class Random
    {
        public static Sudoku Get(Difficulty? difficulty = null)
        {
            return DefaultProblem.Get();
        }
    }
}
