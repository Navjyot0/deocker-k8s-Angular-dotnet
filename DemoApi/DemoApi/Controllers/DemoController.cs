using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("demo")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {
            return "Hello World";
        }

        [HttpPost]
        public void Post()
        {
            var mongoUri = "mongodb+srv://user001:3BPrSrGqUHYs7K6t@cluster0.nrqja1p.mongodb.net/?authSource=admin";
            IMongoClient client;
            IMongoCollection<Recipe> collection;

            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            var dbName = "myDatabase";
            var collectionName = "recipes";

            collection = client.GetDatabase(dbName)
               .GetCollection<Recipe>(collectionName);

            var docs = Recipe.GetRecipes();

            try
            {
                collection.InsertMany(docs);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong trying to insert the new documents." +
                    $" Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public int PrepTimeInMinutes { get; set; }

        public Recipe(string name, List<string> ingredients, int prepTime)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.PrepTimeInMinutes = prepTime;
        }
      
        public static List<Recipe> GetRecipes()
        {
            return new List<Recipe>()
            {
                new Recipe("elotes", new List<string>(){"corn", "mayonnaise", "cotija cheese", "sour cream", "lime" }, 35),
                new Recipe("loco moco", new List<string>(){"ground beef", "butter", "onion", "egg", "bread bun", "mushrooms" }, 54),
                new Recipe("patatas bravas", new List<string>(){"potato", "tomato", "olive oil", "onion", "garlic", "paprika" }, 80),
                new Recipe("fried rice", new List<string>(){"rice", "soy sauce", "egg", "onion", "pea", "carrot", "sesame oil" }, 40),
            };
        }
    }
}