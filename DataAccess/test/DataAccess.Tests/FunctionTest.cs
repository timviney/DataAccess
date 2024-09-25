using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace DataAccess.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string returned
        var function = new Function();
        var context = new TestLambdaContext();
        var upperCase = function.FunctionHandler("hello world", context);

        Assert.Equal("CSharp Lambda up and running !!!", upperCase);
    }
}
