using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExampleApi.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }  // Definindo o tipo como ObjectId

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
