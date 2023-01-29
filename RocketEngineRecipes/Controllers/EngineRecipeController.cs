using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RocketEngineRecipes.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;
using System.Net.Mime;

namespace RocketEngineRecipes.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [ApiVersion("1.0")]//, Deprecated = true)]
    [ApiVersion("2.0")]
    public class EngineRecipeController : ControllerBase
    {
        [HttpGet("getRocketEngineType")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
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
            return Ok(new { RocketEngineTypes = rocketEngineTypes.Take(count), OnboardMissile = onboardMissle });
        }

        [HttpPost("addRocketEngineType")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 404)]
        public ActionResult AddRocketEngineType([FromBody] RocketEngineType rocketEngineType)
        {
            bool badThingsHappend = false;
            if (badThingsHappend)
                return BadRequest();
            addToDb(rocketEngineType);
            return Created("/api/RocketEngineType/{rocketEngineType.Id}", rocketEngineType);
        }
      
        [HttpPut("editEngineType")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public ActionResult EditEngineType(string id, [FromBody] RocketEngineType rocketEngineType)
        {
            var rocketEngineTypeCollection = GetRocketEngineTypeCollection();
            var filter = Builders<RocketEngineType>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = rocketEngineTypeCollection.ReplaceOne(filter, rocketEngineType);
            if (result.IsAcknowledged && result.ModifiedCount == 1)
            {
               return NoContent();
            }
            else
            {
               return BadRequest();
            }
                 
        }
      
        [HttpDelete("{id}/deleteEngineType")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public ActionResult DeleteEngineType(string id, [FromBody] RocketEngineType rocketEngineType)
        {
            var rocketEngineTypeCollection = GetRocketEngineTypeCollection();
            var filter = Builders<RocketEngineType>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = rocketEngineTypeCollection.DeleteOne(filter);
            //return result.IsAcknowledged && result.DeletedCount == 1 ?
            if (result.IsAcknowledged && result.DeletedCount == 1)
            {
                Response.Headers.Add("Location", $"https://localhost:7015/api/EngineRecipe/{id}/deleteEngineType");
                // (ActionResult)NoContent() : BadRequest();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        private IMongoCollection<RocketEngineType> GetRocketEngineTypeCollection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            return client.GetDatabase("sample_mflix").GetCollection<RocketEngineType>("rocketEngineType");
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
    }
}
