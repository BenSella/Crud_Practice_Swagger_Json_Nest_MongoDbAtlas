using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RocketEngineRecipes.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RocketEngineRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineRecipeController : ControllerBase
    {
        [HttpGet("getRocketEngineType")]
        public ActionResult GetRocketEngineType([FromQuery] int count)
        {
            RocketEngineType[] rocketEngineTypes =
            {
                new() {Title = "Chemical"},
                new() {Title = "Solid"},
                new() {Title = "Liquid"},
                new() {Title = "Hybrid"},

            };
            var onboardMissle = new OnboardMissle()
            {
                CompanyName = "SpaceX.",
                MissleName = "FalconHeavy",
                MissleVersion = "7.2"
            };
            foreach (var engineType in rocketEngineTypes)
            {
                addToDb(engineType);
            }
            addToDb(onboardMissle);
            return Ok(rocketEngineTypes.Take(count));
        }

        [HttpPost("addRocketEngineType")]
        public ActionResult AddRocketEngineType([FromBody] RocketEngineType rocketEngineType)
        {
            bool badThingsHappend = false;
            if (badThingsHappend)
                return BadRequest();
            addToDb(rocketEngineType);
            return Ok();
        }
        private void addToDb(object data)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            if (data is RocketEngineType)
            {
                var rocketEngineTypeCollection = client.GetDatabase("sample_mflix").GetCollection<RocketEngineType>("rocketEngineType");
                rocketEngineTypeCollection.InsertOne((RocketEngineType)data);
            }
            if (data is OnboardMissle)
            {
                var onboardMissleCollection = client.GetDatabase("sample_mflix").GetCollection<OnboardMissle>("onboardMissle");
                onboardMissleCollection.InsertOne((OnboardMissle)data);
            }
        }
       
        [HttpPut("editEngineType")]
        public ActionResult EditEngineType() 
        {
            return BadRequest();
        }
        [HttpDelete("{id}/deleteEngineType")]
        public ActionResult DeleteEngineType()
        {
            //if (!engineTypes.Any())
            //{
                return NotFound();
            //}
        }




}
}
