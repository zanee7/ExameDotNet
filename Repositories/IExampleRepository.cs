namespace ExampleApi.Repositories;

using ExampleApi.Models;

public interface IExampleRepository
{
    Task<List<ExampleModel>> GetAllAsync();
    Task<ExampleModel?> GetByIdAsync(string id);
    Task AddAsync(ExampleModel model);
    Task UpdateAsync(string id, ExampleModel model);
    Task DeleteAsync(string id);
}
