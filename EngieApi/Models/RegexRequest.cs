using System.Text.RegularExpressions;

namespace EngieApi.Models
{
    public sealed class RegexRequest
    {
        public string Input { get; set; } = string.Empty;
        public string Pattern { get; set; } = string.Empty;
        
        public RegexOptions Option { get; set; }
    }
}
