using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace SudokuProblems
{
    internal static class DynamoDb
    {
        private const int ItemsInDb = 100;
        private static readonly AmazonDynamoDBClient Client = new();
        
        public static async Task<Sudoku> Get(Difficulty? difficulty)
        {
            System.Random random = new();
            var id = random.Next(ItemsInDb);

            // Initialize the DynamoDB client

            var request = new GetItemRequest
            {
                TableName = "SudokuProblems",
                Key = new Dictionary<string, AttributeValue>
                {
                    { "difficulty", new AttributeValue { S = difficulty.ToString() } },
                    { "id", new AttributeValue { N = id.ToString() } }
                }
            };

            var response = await Client.GetItemAsync(request);

            return new Sudoku(response.Item["problem"].S);
        }

        public static async Task WakeUp()
        {
            // This triggers the static constructor (creating the client)
            // and performs a metadata call to pre-heat the SSL/TCP connection.
            _ = await Client.DescribeTableAsync("SudokuProblems");
        }
    }
}
