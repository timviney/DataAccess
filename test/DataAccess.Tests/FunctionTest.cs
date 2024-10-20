using System.Text.Json;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using DataAccess.Structures;
using SudokuProblems;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace DataAccess.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestNoTable()
    {
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new LambdaRequest()
        {
            Table = DbTable.NA,
            Method = "",
            RequestParameters = JsonSerializer.SerializeToElement("")
        };
        var result = await function.FunctionHandler(request, context);

        var expected = Error.NewReturnValue("Please specify table!");
        var actual = result.Deserialize<ReturnValue<int>>(ApiOptions.Options);

        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task TestSudoku()
    {
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new LambdaRequest()
        {
            Table = DbTable.SudokuProblems,
            Method = "Random",
            RequestParameters = JsonSerializer.SerializeToElement(new SudokuProblemsRequest(Difficulty.Medium))
        };

        var result = await function.FunctionHandler(request, context);
        var resultValue = result.Deserialize<ReturnValue<Sudoku>>(ApiOptions.Options);

        Assert.NotNull(resultValue);
        Assert.True(resultValue.Success);
        Assert.NotNull(resultValue.Result);
        Assert.NotNull(resultValue.Result.Grid);
    }
}