namespace ExampleApi.Repositories;

using MongoDB.Driver;
using ExampleApi.Models;

public class ExampleRepository : IExampleRepository
{
    private readonly IMongoCollection<ExampleModel> _collection;

    public ExampleRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ExampleModel>("Examples");
    }

    public async Task<List<ExampleModel>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<ExampleModel?> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(ExampleModel model)
    {
        await _collection.InsertOneAsync(model);
    }

    public async Task UpdateAsync(string id, ExampleModel model)
    {
        await _collection.ReplaceOneAsync(x => x.Id == id, model);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
