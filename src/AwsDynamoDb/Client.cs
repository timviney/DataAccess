using Amazon;
using Amazon.DynamoDBv2;

namespace AwsDynamoDb
{
    public static class Client
    {
        public static AmazonDynamoDBClient GetClient()
        {
#if RELEASE
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.EUNorth1
            };

            // Should get configured by IAM on Lambda
            var client = new AmazonDynamoDBClient(config);
#else
            // Should get configured by AWS Console
            var client = new AmazonDynamoDBClient();
#endif
            return client;
        }
    }
}
