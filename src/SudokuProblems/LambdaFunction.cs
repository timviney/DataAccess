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
        public static IReturnValue FunctionHandler(LambdaRequest request)
        {
            try
            {
                if (request.Method == "Random")
                {
                    var sudokuRequest = request.RequestParameters.Deserialize<SudokuProblemsRequest>();
                    var problem = Random.Get(sudokuRequest!.Difficulty);
                    return ReturnValue<Sudoku>.NewSuccessReturn(problem);
                }
                else return Error.NewReturnValue($"Do not recognise method {request.Method}!");
            }
            catch (Exception e)
            {
                return Error.NewReturnValue($"ERROR! {e}");
            }
        }
    }
}
