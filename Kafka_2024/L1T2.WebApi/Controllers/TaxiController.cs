using Lib;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace L1T2.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxiController : ControllerBase
    {
        private readonly IProducer _producer;

        public TaxiController()
        {
            _producer = new Producer(GlobalConfig.InputTopicsL1T2[0]);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Signal input)
        {
            var message = JsonSerializer.Serialize(input);
            await _producer.Produce(message, input.Id.ToString());
            return Ok(message);
        }
    }
}
