using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poc.Silverback.Outbox.Application.Events;
using Poc.Silverback.Outbox.Application.Persistance.Contexts;
using Silverback.Messaging.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Silverback.Outbox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPublisher _publisher;
        private readonly KafkaManagementDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublisher publisher, KafkaManagementDbContext dbContext)
        {
            _logger = logger;
            _publisher = publisher;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("/SendOrder")]
        public async Task<IActionResult> SendOrder()
        {
            await _publisher.PublishAsync(new OrderCommand
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now
            });
            //await _dbContext.SaveChangesAsync();

            await _publisher.PublishAsync(new OrderCommand2
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
