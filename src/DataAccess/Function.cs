using System.Text.Json;
using Amazon.Lambda.Core;
using DataAccess.Structures;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(CustomLambdaSerialiser))]

namespace DataAccess;
public class Function
{

    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<JsonElement> FunctionHandler(LambdaRequest input, ILambdaContext context)
    {
        var result = input!.Table switch
        {
            DbTable.NA => Error.NewReturnValue<object>("Please specify table!"),
            DbTable.SudokuProblems => await SudokuProblems.LambdaFunction.FunctionHandler(input),
            _ => throw new ArgumentOutOfRangeException()
        };

        return result.AsJsonElement();
    }
}
