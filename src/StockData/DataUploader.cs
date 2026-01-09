using System.Globalization;

namespace StockData
{
    public static class DataUploader
    {
        private const string DateTimeFormat = "yyyyMMddHHmmss";

        public static async Task ReadAndUpload(string folderPath, string interval, DateTime startMonth)
        {
            var possibleSymbolsToRead = Enum.GetNames(typeof(Symbol)).ToHashSet();
            foreach (var file in Directory.GetFiles(folderPath))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var symbol = fileName.Split('.')[0].ToUpper();

                if (!possibleSymbolsToRead.Contains(symbol)) continue;

                var data = ReadData(file, startMonth);

                await Uploader.UploadFilesAsync(data, interval, symbol);
            }
        }

        private static List<MarketDataEntry> ReadData(string file, DateTime startMonth)
        {
            using var csvReader = new StreamReader(File.OpenRead(file));
            var result = new List<MarketDataEntry>();

            csvReader.ReadLine(); // skip headers
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(',');

                var date = values[2];
                var time = values[3];
                var open = values[4];
                var high = values[5];
                var low = values[6];
                var close = values[7];
                var vol = values[8];

                var dataPoint = new MarketDataEntry(DateTime.ParseExact(date+time, DateTimeFormat, CultureInfo.InvariantCulture), decimal.Parse(open),
                    decimal.Parse(high), decimal.Parse(low), decimal.Parse(close), (int)Math.Round(double.Parse(vol)));

                if (dataPoint.DateTime <  startMonth) continue;

                result.Add(dataPoint);
            }

            return result;
        }
        
        private enum Symbol
        {
            // Technology
            AAPL,    // Apple Inc.
            MSFT,    // Microsoft Corporation
            GOOGL,   // Alphabet Inc. (Google Class A)
            SAP,     // SAP SE
            AUTO,    // Auto Trader Group PLC
            REL,     // Relx PLC
            TSLA,    // Tesla Inc.

            // Financial
            JPM,     // JPMorgan Chase & Co.
            HSBA,    // HSBC Holdings PLC
            LLOY,    // Lloyds Banking Group PLC
            BARC,    // Barclays PLC
            PRU,     // Prudential PLC
            LGEN,    // Legal & General Group PLC
            SAN,     // Banco Santander SA

            // Healthcare
            JNJ,     // Johnson & Johnson
            PFE,     // Pfizer Inc.
            AZN,     // AstraZeneca PLC
            GSK,     // GlaxoSmithKline PLC
            MRK,     // Merck & Co., Inc.

            // Consumer Goods
            PG,      // Procter & Gamble Co.
            KO,      // Coca-Cola Company
            PEP,     // PepsiCo, Inc.
            ULVR,    // Unilever PLC
            DGE,     // Diageo PLC

            // Energy
            BP,      // BP PLC
            XOM,     // Exxon Mobil Corporation
            CVX,     // Chevron Corporation
            SSE,     // SSE PLC

            // Telecommunications
            VOD,     // Vodafone Group PLC
            T,       // AT&T Inc.
            VZ,      // Verizon Communications Inc.

            // Retail
            WMT,     // Walmart Inc.
            TSCO,    // Tesco PLC
            HD,      // Home Depot, Inc.
            TGT,     // Target Corporation
            COST,    // Costco Wholesale Corporation
            SBRY,    // Sainsbury's PLC
            AMZN,    // Amazon.com Inc.

            // Industrials
            BA,      // Boeing Company
            GE,      // General Electric Company
            CAT,     // Caterpillar Inc.
            RR,      // Rolls-Royce Holdings PLC
            AIR,     // Airbus SE

            // Consumer Discretionary
            DIS,     // Walt Disney Company
            NFLX,    // Netflix Inc.
            NKE,     // Nike, Inc.
            SBUX,    // Starbucks Corporation
            MCD,     // McDonald's Corporation

            // Semiconductors
            AMD,     // Advanced Micro Devices, Inc.
            NVDA,    // NVIDIA Corporation
            INTC,    // Intel Corporation
            TSM,     // Taiwan Semiconductor Manufacturing Company
        }
    }
}
