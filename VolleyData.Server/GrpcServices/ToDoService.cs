using Microsoft.EntityFrameworkCore;
using VolleyData.Server.Data;
using VolleyData.Shared.DTOs;
using VolleyData.Shared.Services;

namespace VolleyData.Server.GrpcServices
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _dataContext;
        private Stack<ToDoData> _undoStack;

        public ToDoService(ToDoDbContext dataContext)
        {
            _dataContext = dataContext;
            _undoStack = new Stack<ToDoData>();
        }

        public Task<ToDoRequestResponse> AddToDoItemAsync(ToDoData data)
        {
            _dataContext.ToDoDbItems.Add(data);
            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Added Successfully"
                });
            }

            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public async Task<ToDoRequestResponse> UpdateToDoItemAsync(ToDoData data)
        {
            var item = await _dataContext.ToDoDbItems
                .FirstOrDefaultAsync(toDoData => toDoData.Id == data.Id);
            if (item == null)
            {
                return new ToDoRequestResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    StatusMessage = "Item not found."
                };
            }

            

            item.Title = data.Title;
            item.Description = data.Description;
            item.Status = data.Status;
            item.AttackTotal = data.AttackTotal;
            item.AttackError = data.AttackError;
            item.AttackBlock = data.AttackBlock;
            item.AttackKill = data.AttackKill;
            item.ReceiveTotal = data.ReceiveTotal;
            item.ReceiveError = data.ReceiveError;
            item.ReceivePositiv = data.ReceivePositiv;
            item.ReceiveExcellent = data.ReceiveExcellent;
            item.ServeTotal = data.ServeTotal;
            item.ServeError = data.ServeError;
            item.ServeKill = data.ServeKill;
            item.ActionsTotal = data.ActionsTotal;
            item.ActionsError = data.ActionsError;
            item.ErrorPercentage = data.ErrorPercentage;

            await _dataContext.SaveChangesAsync();
            return new ToDoRequestResponse()
            {
                Status = true,
                StatusCode = 100,
                StatusMessage = "Added Successfully"
            };
        }

        public Task<ToDoRequestResponse> DeleteToDoItemAsync(ToDoIdQuery idQuery)
        {
            var item = _dataContext.ToDoDbItems
                .Single(toDoData => toDoData.Id == idQuery.Id);

            _dataContext.ToDoDbItems.Remove(item);

            var result = _dataContext.SaveChanges();

            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Deleted Successfully"
                });
            }

            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public Task<ToDoRequestResponse> ClearAllAsync()
        {
            _dataContext.ToDoDbItems.RemoveRange(_dataContext.ToDoDbItems);

            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                _undoStack.Clear();
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Deleted Successfully"
                });
            }
            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        // Reset all
        public Task<ToDoRequestResponse> ResetAllAsync()
        {
            var items = _dataContext.ToDoDbItems;
            foreach (var item in items)
            {
                item.AttackTotal = 0;
                item.AttackError = 0;
                item.AttackBlock = 0;
                item.AttackKill = 0;
                item.ReceiveTotal = 0;
                item.ReceiveError = 0;
                item.ReceivePositiv = 0;
                item.ReceiveExcellent = 0;
                item.ServeTotal = 0;
                item.ServeError = 0;
                item.ServeKill = 0;
                item.ActionsTotal = 0;
                item.ActionsError = 0;
                item.ErrorPercentage = 0;
            }

            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Reset Successfully"
                });
            }
            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public Task<ToDoRequestResponse> UndoAsync()
        {
            
            _undoStack.TryPop(out var item);

            if (item == null)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    StatusMessage = "Undo not possible"
                });
            }
            
            var itemToUpdate = _dataContext.ToDoDbItems
                .Single(toDoData => toDoData.Id == item.Id);

            itemToUpdate.Title = item.Title;
            itemToUpdate.Description = item.Description;
            itemToUpdate.Status = item.Status;
            itemToUpdate.AttackTotal = item.AttackTotal;
            itemToUpdate.AttackError = item.AttackError;
            itemToUpdate.AttackBlock = item.AttackBlock;
            itemToUpdate.AttackKill = item.AttackKill;
            itemToUpdate.ReceiveTotal = item.ReceiveTotal;
            itemToUpdate.ReceiveError = item.ReceiveError;
            itemToUpdate.ReceivePositiv = item.ReceivePositiv;
            itemToUpdate.ReceiveExcellent = item.ReceiveExcellent;
            itemToUpdate.ServeTotal = item.ServeTotal;
            itemToUpdate.ServeError = item.ServeError;
            itemToUpdate.ServeKill = item.ServeKill;
            itemToUpdate.ActionsTotal = item.ActionsTotal;
            itemToUpdate.ActionsError = item.ActionsError;
            itemToUpdate.ErrorPercentage = item.ErrorPercentage;

            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Undo Successfully"
                });
            }
            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });

        }


        public async Task<List<ToDoData>> GetToDosAsync()
        {
            return await _dataContext.ToDoDbItems.ToListAsync();
        }
    }
}