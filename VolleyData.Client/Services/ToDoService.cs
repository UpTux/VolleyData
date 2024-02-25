using VolleyData.Shared.DTOs;
using VolleyData.Shared.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Index = VolleyData.Client.Pages.Index;

namespace VolleyData.Client.Services
{
    public class ToDoService
    {
        private readonly IToDoService _toDoService;
        private Stack<ToDoData> _undoStack;

        public ToDoService(IToDoService todoService)
        {
            _toDoService = todoService;
            _undoStack = new Stack<ToDoData>();
        }

        public Index Index { get; set; }

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

        public async Task UndoAsync()
        {
            var hasItem = _undoStack.TryPop(out ToDoData? data);

            if (hasItem)
            {
                await _toDoService.UpdateToDoItemAsync(data);
                await Task.Delay(100);
                await Index.RefreshAsync();
                
            }
            
        }

        public void BackupItem(ToDoData clone)
        {
            _undoStack.Push(clone);
        }
    }
}