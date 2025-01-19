using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3.Model;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.Serialization;

namespace StockData
{
    public class Downloader : IDisposable
    {
        private readonly IAmazonS3 _s3Client = new AmazonS3Client(S3DataStore.BucketRegion);

        public async Task<Dictionary<DateTime, MarketDataEntry>> GetDataAsync(string symbol, string interval, DateTime from, DateTime to)
        {
            try
            {
                var fromMonth = new DateTime(from.Year, from.Month, 1);
                var toMonth = new DateTime(to.Year, to.Month, 1);

                var months = new List<DateTime>();
                for (var i = fromMonth; i <= toMonth; i = i.AddMonths(1))
                {
                    months.Add(i);
                }

                var results = new Dictionary<DateTime, MarketDataEntry>();

                foreach (var month in months)
                {
                    var request = new GetObjectRequest()
                    {
                        BucketName = S3DataStore.BucketName,
                        Key = S3DataStore.GetName(month, interval, symbol)
                    };
                    using var response = await _s3Client.GetObjectAsync(request);

                    await ReadCsvWithinTimeRange(from, to, response, results);
                }

                return results.ToDictionary();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task ReadCsvWithinTimeRange(DateTime from, DateTime to, GetObjectResponse response,
            Dictionary<DateTime, MarketDataEntry> results)
        {
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var csvReader = new StreamReader(memoryStream);

            await csvReader.ReadLineAsync(); // skip headers
            while (!csvReader.EndOfStream)
            {
                var line = await csvReader.ReadLineAsync();
                var values = line.Split(',');

                var dateTime = values[0];
                var open = values[1];
                var high = values[2];
                var low = values[3];
                var close = values[4];
                var vol = values[5];

                var dataPoint = new MarketDataEntry(DateTime.Parse(dateTime), decimal.Parse(open),
                    decimal.Parse(high), decimal.Parse(low), decimal.Parse(close), (int)Math.Round(double.Parse(vol)));

                if (dataPoint.DateTime < from || dataPoint.DateTime > to) continue;

                results.Add(dataPoint.DateTime, dataPoint);
            }
        }

        public void Dispose()
        {
            _s3Client?.Dispose();
        }
    }
}
