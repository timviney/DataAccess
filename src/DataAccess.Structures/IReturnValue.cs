using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Structures
{
    public interface IReturnValue
    {
        int StatusCode { get; set; }
        bool Success { get; set; }
        JsonElement AsJsonElement();
    }
}
