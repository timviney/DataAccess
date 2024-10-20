using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Structures
{
    public class ReturnValue<TResult> : IReturnValue
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }

        public TResult? Result { get; set; }
        public Error? Error { get; set; }

        public static ReturnValue<TResult> NewSuccessReturn(TResult result)
        {
            return new ReturnValue<TResult>()
            {
                StatusCode = 200,
                Success = true,
                Result = result,
                Error = null
            };
        }

        public JsonElement AsJsonElement()
        {
            // Do this at a subclass level so that all parameters come through
            return JsonSerializer.SerializeToElement(this, ApiOptions.Options);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as ReturnValue<TResult>;
            return other != null && Equals(other);
        }

        protected bool Equals(ReturnValue<TResult> other)
        {
            return StatusCode == other.StatusCode &&
                   Success == other.Success &&
                   EqualityComparer<TResult?>.Default.Equals(Result, other.Result) &&
                   Nullable.Equals(Error, other.Error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StatusCode, Success, Result, Error);
        }
    }
}
