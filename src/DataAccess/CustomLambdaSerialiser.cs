using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Text.Json;
using DataAccess.Structures;

public class CustomLambdaSerialiser() : DefaultLambdaJsonSerializer(options =>
{
    options.PropertyNamingPolicy = ApiOptions.Options.PropertyNamingPolicy;
    options.PropertyNameCaseInsensitive = ApiOptions.Options.PropertyNameCaseInsensitive;
    options.Converters.Add(ApiOptions.Options.Converters[0]);
});