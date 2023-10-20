using System.Text.Json;

namespace EngieApi.Models
{
    public sealed class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } 

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
