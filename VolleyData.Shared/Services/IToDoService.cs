using System.ServiceModel;
using VolleyData.Shared.DTOs;

namespace VolleyData.Shared.Services
{
    [ServiceContract]
    public interface IToDoService
    {
        [OperationContract]
        Task<List<ToDoData>> GetToDosAsync();
        
        [OperationContract]
        Task<ToDoRequestResponse> AddToDoItemAsync(ToDoData data);
        
        [OperationContract]
        Task<ToDoRequestResponse> UpdateToDoItemAsync(ToDoData data);
        
        [OperationContract]
        Task<ToDoRequestResponse> DeleteToDoItemAsync(ToDoIdQuery idQuery);
        [OperationContract]
        Task<ToDoRequestResponse> ClearAllAsync();

        [OperationContract]
        Task<ToDoRequestResponse> ResetAllAsync();

        [OperationContract]
        Task<ToDoRequestResponse> UndoAsync();
    }
}
