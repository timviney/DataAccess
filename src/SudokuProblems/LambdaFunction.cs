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
                switch (request.Method)
                {
                    case "random":
                    {
                        Console.WriteLine(JsonSerializer.Serialize(request));
                        var sudokuRequest = request.RequestParameters.Deserialize<SudokuProblemsRequest>(ApiOptions.Options);
                        var problem = await Random.Get(sudokuRequest!.Difficulty);
                        return ReturnValue<Sudoku>.NewSuccessReturn(problem);
                    }
                    case "wakeUp":
                        await DynamoDb.WakeUp();
                        return ReturnValue<string>.NewSuccessReturn("I'm awake!");
                    default:
                        return Error.NewReturnValue<Sudoku>($"Do not recognise method {request.Method}!");
                }
            }
            catch (Exception e)
            {
                return Error.NewReturnValue<Sudoku>($"ERROR! {request.RequestParameters} {e}");
            }
        }
    }
}
