using StockData;

namespace DataTidy
{
    public static class FixStockData
    {
        public static async Task Fix()
        {
            using var downloader = new Downloader();
            foreach (var symbol in Enum.GetValues<Symbol>())
            {
                var data = await downloader.GetDataAsync(symbol.ToString(), 
                    "60min", new DateTime(2024,04,01), new DateTime(2024,04,30, 23, 59, 59));

                MarketDataEntry? previousVal = null;
                var dateTimes = data.Keys.ToArray();
                bool updated = false;
                foreach (var dateTime in dateTimes)
                {
                    var val = data[dateTime];

                    if (previousVal != null)
                    {
                        // check if value is 100 times too small
                        if (Math.Abs(previousVal.Open * 0.01m - val.Open) < 0.05m * previousVal.Open)
                        {
                            // If it is, multiply by 100 and replace.
                            val = val with
                            {
                                Open = val.Open * 100,
                                High = val.High * 100,
                                Low = val.Low * 100,
                                Close = val.Close * 100,
                            };

                            data[dateTime] = val;

                            updated = true;
                        }
                    }

                    previousVal = val;
                }

                if (!updated) continue;

                await Uploader.UploadFilesAsync(data.Values.ToList(), "60min", symbol.ToString());
            }
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
