﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTidy
{
    public class Program
    {
        public static async Task Main()
        {
            await FixStockData.Fix();
        }
    }
}
