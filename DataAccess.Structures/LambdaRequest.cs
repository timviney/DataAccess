using System.Text.Json;

namespace DataAccess.Structures
{
    [Serializable]
    public class LambdaRequest
    {
        public DbTable Table { get; set; }
        public string Method { get; set; }
        public JsonElement RequestParameters { get; set; }
    }
}
