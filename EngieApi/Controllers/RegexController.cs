using EngieApi.Logging;
using EngieApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegexController : ControllerBase
    {
        private readonly ILogging _logger;

        public RegexController(ILogging logger)
        { 
            _logger = logger;
        }

        // GET: api/<RegexController>
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> Post([FromBody] RegexRequest request)
        {
            // alice@example.com, bob@example.com, carol@example.com
            // ([\\w]+)@
            if (string.IsNullOrEmpty(request.Input) || string.IsNullOrEmpty(request.Pattern)) 
            {
                _logger.Log("error", "Invalid search");
                return BadRequest();
            }

            _logger.Log("search", request.Pattern);

            MatchCollection matches = Regex.Matches(request.Input, request.Pattern, request.Option);

            if (!matches.Any()) 
            {
                return NotFound();
            }

            return matches.Select(m => m.Groups[1].Value).ToList();
        }

        // GET: api/<RegexController>
        [HttpGet("Replace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Replace(string input, string pattern)
        {
            var result = Regex.Replace(input, pattern, "$1 is at ");
            _logger.Log("replace", pattern);
            return Ok(result);
        }

        // GET: api/<RegexController>
        [HttpGet("IsMatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsMatch(string input, string pattern, RegexOptions option)
        {
            var result = Regex.IsMatch(input, pattern, option);
            _logger.Log("isMatch", pattern);
            return Ok(result);
        }

    }
}
