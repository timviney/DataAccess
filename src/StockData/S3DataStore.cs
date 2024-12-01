using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;

namespace StockData
{
    public static class S3DataStore
    {
        public const string BucketName = "stockdata-algotrader";
        public static readonly RegionEndpoint BucketRegion = RegionEndpoint.EUNorth1;

        public static string GetName(DateTime month, string interval, string symbol)
        {
            return $"{symbol}/{interval}/{month:yy-MM}.csv";
        }
    }
}
