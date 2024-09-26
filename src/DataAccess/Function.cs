using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Lambda.Core;
using DataAccess.Structures;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DataAccess;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        var request = JsonSerializer.Deserialize<LambdaRequest>(input);

        return request!.Table switch
        {
            DbTable.NA => "Please specify table!",
            DbTable.SudokuProblems => SudokuProblems.LambdaFunction.FunctionHandler(request),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}