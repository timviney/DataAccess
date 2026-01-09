using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace StockData
{
    public static class Uploader
    {
        private static readonly IAmazonS3 S3Client = new AmazonS3Client(S3DataStore.BucketRegion);

        public static async Task UploadFilesAsync(List<MarketDataEntry> entries, string interval, string symbol, bool uploadPotentiallyIncompleteMonths = false)
        {
            try
            {
                var entriesForMonth = new List<MarketDataEntry>();
                var month = new DateTime(entries[0].DateTime.Year, entries[0].DateTime.Month, 1);

                foreach (var entry in entries)
                {
                    var monthOfEntry = new DateTime(entry.DateTime.Year, entry.DateTime.Month, 1);
                    if (monthOfEntry != month)
                    {
                        await UploadCsvToS3Async(entriesForMonth, month, interval, symbol);
                        entriesForMonth.Clear();
                        month = monthOfEntry;
                    }

                    entriesForMonth.Add(entry);
                }

                if (uploadPotentiallyIncompleteMonths && entriesForMonth.Count > 0)
                {
                    await UploadCsvToS3Async(entriesForMonth, month, interval, symbol);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}'");
            }
        }

        private static async Task UploadCsvToS3Async(List<MarketDataEntry> data, DateTime month, string interval, string symbol)
        {
            try
            {
                var csvContent = GenerateCsvContent(data);

                using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

                var putRequest = new PutObjectRequest
                {
                    BucketName = S3DataStore.BucketName,
                    Key = S3DataStore.GetName(month, interval, symbol),
                    InputStream = memoryStream,
                    ContentType = "text/csv"
                };

                var response = await S3Client.PutObjectAsync(putRequest);
                Console.WriteLine($"Uploaded {putRequest.Key}, status: {response.HttpStatusCode}");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}'");
            }
        }

        private static string GenerateCsvContent(List<MarketDataEntry> data)
        {
            var sb = new StringBuilder();
            
            // Add CSV header
            sb.AppendLine("<DATETIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>");

            // Add rows
            foreach (var dataEntry in data)
            {
                sb.AppendLine($"{dataEntry.DateTime},{dataEntry.Open},{dataEntry.High},{dataEntry.Low},{dataEntry.Close},{dataEntry.Volume}");
            }

            return sb.ToString();
        }
    }
}
