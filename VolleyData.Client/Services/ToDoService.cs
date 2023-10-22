using VolleyData.Shared.DTOs;
using VolleyData.Shared.Services;

namespace VolleyData.Client.Services
{
    public class ToDoService
    {
        private readonly IToDoService _toDoService;

        public ToDoService(IToDoService todoService)
        {
            _toDoService = todoService;
        }

        public Task AddToDoData(ToDoData data)
        {
            return _toDoService.AddToDoItemAsync(data);
        }

        public Task UpdateToDoData(ToDoData data)
        {
            return _toDoService.UpdateToDoItemAsync(data);
        }

        public Task DeleteDataAsync(string id)
        {
            return _toDoService.DeleteToDoItemAsync(new ToDoIdQuery() { Id = Convert.ToInt32(id) });
        }

        public async Task<List<ToDoData>> GetToDoListAsync()
        {
            return await _toDoService.GetToDosAsync();
        }

        public async Task ClearAllAsync()
        {
            await _toDoService.ClearAllAsync();
        }

        public async Task ResetAllAsync()
        {
            await _toDoService.ResetAllAsync();
        }
    }
}