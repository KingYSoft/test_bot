namespace dotnet_api.Services;

public interface ISampleService
{
    Task<IEnumerable<Models.SampleModel>> GetAllAsync();
    Task<Models.SampleModel?> GetByIdAsync(int id);
    Task<Models.SampleModel> CreateAsync(Models.SampleModel model);
    Task<Models.SampleModel?> UpdateAsync(int id, Models.SampleModel model);
    Task<bool> DeleteAsync(int id);
}

public class SampleService : ISampleService
{
    private readonly List<Models.SampleModel> _samples;
    
    public SampleService()
    {
        _samples = new List<Models.SampleModel>
        {
            new Models.SampleModel { Id = 1, Name = "Sample 1", Description = "This is the first sample" },
            new Models.SampleModel { Id = 2, Name = "Sample 2", Description = "This is the second sample" }
        };
    }
    
    public Task<IEnumerable<Models.SampleModel>> GetAllAsync()
    {
        return Task.FromResult(_samples.AsEnumerable());
    }
    
    public Task<Models.SampleModel?> GetByIdAsync(int id)
    {
        var item = _samples.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(item);
    }
    
    public Task<Models.SampleModel> CreateAsync(Models.SampleModel model)
    {
        model.Id = _samples.Max(s => s.Id) + 1;
        _samples.Add(model);
        return Task.FromResult(model);
    }
    
    public Task<Models.SampleModel?> UpdateAsync(int id, Models.SampleModel model)
    {
        var existing = _samples.FirstOrDefault(s => s.Id == id);
        if (existing != null)
        {
            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.IsActive = model.IsActive;
        }
        return Task.FromResult(existing);
    }
    
    public Task<bool> DeleteAsync(int id)
    {
        var existing = _samples.FirstOrDefault(s => s.Id == id);
        if (existing != null)
        {
            _samples.Remove(existing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}