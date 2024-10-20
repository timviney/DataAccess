using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Structures;

namespace SudokuProblems
{
    public static class LambdaFunction
    {
        public static async Task<IReturnValue> FunctionHandler(LambdaRequest request)
        {
            try
            {
                if (request.Method == "random")
                {
                    Console.WriteLine(JsonSerializer.Serialize(request));
                    var sudokuRequest = request.RequestParameters.Deserialize<SudokuProblemsRequest>(ApiOptions.Options);
                    var problem = await Random.Get(sudokuRequest!.Difficulty);
                    return ReturnValue<Sudoku>.NewSuccessReturn(problem);
                }
                else return Error.NewReturnValue<Sudoku>($"Do not recognise method {request.Method}!");
            }
            catch (Exception e)
            {
                return Error.NewReturnValue<Sudoku>($"ERROR! {request.RequestParameters} {e}");
            }
        }
    }
}
