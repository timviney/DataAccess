namespace SudokuProblems
{
    internal class Random
    {
        public static async Task<Sudoku> Get(Difficulty? difficulty = null)
        {
            return await DynamoDb.Get(difficulty);
        }
    }
}
