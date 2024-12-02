namespace ExampleApi.Services;

using ExampleApi.Models;
using ExampleApi.Repositories;

public class ExampleService : IExampleService
{
    private readonly IExampleRepository _repository;

    public ExampleService(IExampleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ExampleModel>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ExampleModel?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(ExampleModel model)
    {
        await _repository.AddAsync(model);
    }

    public async Task UpdateAsync(string id, ExampleModel model)
    {
        await _repository.UpdateAsync(id, model);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}
