using MongoDB.Bson;
namespace RocketEngineRecipes.Models
{
    public class RocketEngineType
    {
        private DateTime update;
        public OnboardMissle OnboardMissle { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public IEnumerable<string> Ingredients { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> HowToMAke { get; set; } = Enumerable.Empty<string>();
        public DateTime Update { get => update; set => update = value; }
    }
    public class OnboardMissle 
    {
        public string CompanyName { get; set; }
        public string MissleName { get; set;}
        public string MissleVersion { get; set; }
    }
}
// public string Title { get; set; }
// public string Description { get; set; }

// }

