using System.Text.Json;

namespace DataAccess.Structures
{
    [Serializable]
    public class LambdaRequest
    {
        public required DbTable Table { get; set; }
        public required string Method { get; set; }
        public JsonElement RequestParameters { get; set; }
    }
}
