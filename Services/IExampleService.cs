namespace ExampleApi.Services;

using ExampleApi.Models;

public interface IExampleService
{
    Task<List<ExampleModel>> GetAllAsync();
    Task<ExampleModel?> GetByIdAsync(string id);
    Task AddAsync(ExampleModel model);
    Task UpdateAsync(string id, ExampleModel model);
    Task DeleteAsync(string id);
}
