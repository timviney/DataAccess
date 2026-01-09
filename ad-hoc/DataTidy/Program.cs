using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData;

namespace DataTidy
{
    public class Program
    {
        public static async Task Main()
        {
            await DataUploader.ReadAndUpload(@"C:\Users\tcvin\Downloads\5_uk_txt (1)\data\5 min\uk\lse stocks", "5min", new DateTime(2025, 8, 1));
        }
    }
}
