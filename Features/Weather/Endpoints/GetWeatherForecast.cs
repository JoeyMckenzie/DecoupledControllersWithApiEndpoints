using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace DecoupledControllersWithApiEndpoints.Features.Weather.Endpoints
{
    [Route("api/weather")]
    public class GetWeatherForecast : BaseEndpoint<IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public override ActionResult<IEnumerable<WeatherForecast>> Handle()
        {
            var rng = new Random();

            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(forecasts);
        }
    }
}
