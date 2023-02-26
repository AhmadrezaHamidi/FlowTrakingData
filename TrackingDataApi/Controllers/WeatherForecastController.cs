using Microsoft.AspNetCore.Mvc;
using RepositoryEfCore.IReposetory;
using System.Reflection.Metadata;

namespace TrackingDataApi.Controllers
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
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastController(IUnitOfWork unitOfWork,ILogger<WeatherForecastController> logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {


            var repo = _unitOfWork.GetRepository<UserEntity>();
            var user = new UserEntity("ddddd", "ddddd", "ddddd", "ddddd");
            repo.Insert(user);
            _unitOfWork.SaveChanges();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}