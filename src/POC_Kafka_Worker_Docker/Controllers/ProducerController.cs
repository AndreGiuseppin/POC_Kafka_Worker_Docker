using Kafka.Producer.DTOs.Request;
using Kafka.Producer.Interfaces.Producer;
using Microsoft.AspNetCore.Mvc;

namespace POC_Kafka_Worker_Docker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IPixProducer _pixProducer;

        public ProducerController(IPixProducer pixProducer)
        {
            _pixProducer = pixProducer;
        }

        [HttpPost]
        public async Task<IActionResult> Producer([FromBody] PixProducerRequest pixProducerRequest)
        {
            await _pixProducer.Producer(pixProducerRequest);

            return Ok();
        }
    }
}