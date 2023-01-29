using MongoDB.Driver;
using RocketEngineRecipes.Models;

namespace RocketEngineRecipes
{
   
    public class ServiceCollectionExtensions
    {
    //    MongoClient client;
    //    public ServiceCollectionExtensions()
    //    {
    //        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    //        var connectionString = config.GetConnectionString("MongoDB");
    //        client = new MongoClient(connectionString);
    //        /*
    //        var rocketEnginelist = client.GetDatabase("sample_mflix").GetCollection<RocketEngineType>("rocketEngineType");
    //        List<string> list = new List<string>();
    //        list.Add("123");
    //        rocketEnginelist.InsertOne(new RocketEngineType());

    //        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    //        var connectionString = config.GetConnectionString("MongoDB");
    //        var client = new MongoClient(connectionString);
    //        var rocketEngineTypeCollection = client.GetDatabase("sample_mflix").GetCollection<RocketEngineType>("rocketEngineType");
    //        rocketEngineTypeCollection.InsertOne(rocketEngineType);
    //        return Ok();
    //        */
    //    }
    }
}
