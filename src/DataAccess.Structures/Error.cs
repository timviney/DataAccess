using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Structures
{
    [Serializable]
    public struct Error(string message, string full)
    {
        public Error() : this(null,null) { }
        public string Message { get; set; } = message;
        public string Full { get; set; } = full;

        public static ReturnValue<TExpectedReturn> NewReturnValue<TExpectedReturn>(string message, string? full = null) 
            => new ReturnValue<TExpectedReturn>()
        {
            StatusCode = 400,
            Success = false,
            Result = default,
            Error = new Error(message, full ?? message)
        };
    }
}
