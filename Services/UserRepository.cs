using MongoDB.Driver;
using ExampleApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace ExampleApi.Services
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        // CRUD - Create
        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        // CRUD - Read
        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        // CRUD - Update
        public async Task UpdateUserAsync(string id, User user)
        {
            // Converte o id de string para ObjectId
            var objectId = new ObjectId(id);
            
            // Substitui o documento onde o Id corresponde
            await _users.ReplaceOneAsync(u => u.Id == objectId, user);
        }

        // CRUD - Delete
        public async Task DeleteUserAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _users.DeleteOneAsync(u => u.Id == objectId);
        }
    }
}
