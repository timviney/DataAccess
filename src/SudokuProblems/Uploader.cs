using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace SudokuProblems
{
    internal static class Uploader
    {
        private static async Task Run()
        {
            var client = new AmazonDynamoDBClient();
            var filePath = "C:/Working/medium2.txt"; // Path to the file with sudokus inside
            var startingId = 0;
            var maxSudokus = 100;

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var count = 0;

                    while (await reader.ReadLineAsync() is { } line && count < maxSudokus)
                    {
                        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2)
                        {
                            continue;
                        }

                        var problem = parts[1]; // The Sudoku problem is the second value of txt file

                        // Create a new item for DynamoDB
                        var request = new PutItemRequest
                        {
                            TableName = "SudokuProblems",
                            Item = new Dictionary<string, AttributeValue>
                            {
                                { "id", new AttributeValue { N = startingId.ToString() } },
                                { "difficulty", new AttributeValue { S = "medium" } },
                                { "problem", new AttributeValue { S = problem } }
                            }
                        };

                        await client.PutItemAsync(request);
                        Console.WriteLine($"Uploaded Sudoku {startingId}: {problem}");

                        startingId++;
                        count++;
                    }
                }
                Console.WriteLine($"Uploaded {maxSudokus} Sudoku problems to the table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
