using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Structures
{
    [Serializable]
    public struct Error(string message, string full)
    {
        public string Message { get; } = message;
        public string Full { get; } = full;

        public static IReturnValue NewReturnValue(string message, string? full = null) => new ReturnValue<int>()
        {
            StatusCode = 400,
            Success = false,
            Result = 0,
            Error = new Error(message, full ?? message)
        };
    }
}
