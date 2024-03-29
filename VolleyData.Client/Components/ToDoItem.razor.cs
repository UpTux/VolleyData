using Microsoft.AspNetCore.Components;
using VolleyData.Client.Services;
using VolleyData.Shared.DTOs;

namespace VolleyData.Client.Components
{
    public partial class ToDoItem 
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;

        [Parameter] public ToDoData Item { get; set; } = default!;

        [Parameter] public EventCallback<bool> ToDoItemChanged { get; set; }

        

        private string _cssClass = "item";

        protected override void OnInitialized()
        {
            _cssClass = Item.Status ? $"item checked" : "item";
            base.OnInitialized();
        }

        private async Task UpdateTaskAsync()
        {
            //Item.Status = true;
            Item.ActionsTotal = Item.AttackTotal + Item.ReceiveTotal + Item.ServeTotal;
            Item.ActionsError = Item.AttackError + Item.AttackBlock + Item.ReceiveError + Item.ServeError;
            Item.ErrorPercentage = (double)Item.ActionsError / (double)Item.ActionsTotal * 100;
            _cssClass = Item.Status ? $"item checked" : "item";
            await ToDoService.UpdateToDoData(Item);
            await ToDoItemChanged.InvokeAsync(true);
        }

        private async Task DeleteTask()
        {
            await ToDoService.DeleteDataAsync(Item.Id.ToString());
            await ToDoItemChanged.InvokeAsync(true);
        }

        private async Task AddAttackTotal()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackError()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.AttackError++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackBlock()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.AttackBlock++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackKill()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.AttackKill++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveTotal()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveError()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ReceiveError++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceivePositiv()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ReceivePositiv++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveExcellent()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ReceiveExcellent++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeTotal()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeError()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ServeError++;
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeKill()
        {
            ToDoService.BackupItem(Item.Clone());
            Item.ServeKill++;
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }
    }
}