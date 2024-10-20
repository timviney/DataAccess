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
        public static async Task<Sudoku> Get(Difficulty? difficulty)
        {
            System.Random random = new();
            var id = random.Next(ItemsInDb);

            // Initialize the DynamoDB client
            var client = new AmazonDynamoDBClient();

            var request = new GetItemRequest
            {
                TableName = "SudokuProblems",
                Key = new Dictionary<string, AttributeValue>
                {
                    { "difficulty", new AttributeValue { S = difficulty.ToString() } },
                    { "id", new AttributeValue { N = id.ToString() } }
                }
            };

            var response = await client.GetItemAsync(request);

            return new Sudoku(response.Item["problem"].S);
        }
    }
}
