using System.Text.Json;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using DataAccess.Structures;

namespace DataAccess.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new LambdaRequest()
        {
            Table = DbTable.NA,
            Method = "",
            RequestParameters = JsonSerializer.SerializeToElement("")
        };
        var input = JsonSerializer.Serialize(request);
        var result = function.FunctionHandler(input, context);

        Assert.Equal("Please specify table!", result);
    }
}