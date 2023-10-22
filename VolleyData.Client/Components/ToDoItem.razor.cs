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
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackError()
        {
            Item.AttackError++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackBlock()
        {
            Item.AttackBlock++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddAttackKill()
        {
            Item.AttackKill++;
            Item.AttackTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveTotal()
        {
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveError()
        {
            Item.ReceiveError++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceivePositiv()
        {
            Item.ReceivePositiv++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddReceiveExcellent()
        {
            Item.ReceiveExcellent++;
            Item.ReceiveTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeTotal()
        {
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeError()
        {
            Item.ServeError++;
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }

        private async Task AddServeKill()
        {
            Item.ServeKill++;
            Item.ServeTotal++;
            await UpdateTaskAsync();
        }
    }
}