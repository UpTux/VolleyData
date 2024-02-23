using Microsoft.AspNetCore.Components;
using MudBlazor;
using VolleyData.Client.Components;
using VolleyData.Client.Services;
using VolleyData.Shared.DTOs;
using static MudBlazor.CategoryTypes;

namespace VolleyData.Client.Pages
{
    public partial class Index
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private List<ToDoData> _toDoItems = new();
        private List<Summary> _summary = new();
        
        private bool _readOnly;
        private List<string> _events = new();
        private bool _editTriggerRowClick;
        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
            await RefreshAsync();
        }

        private async Task OpenDialog()
        {
            var reference = DialogService.Show<ToDoItemEditor>("Spieler hinzufügen");
            var result = await reference.Result;
            if (result is null || !result.Canceled)
            {
                await RefreshAsync();
            }
        }

        private async Task RefreshAsync()
        {
            await LoadTodosFromServerAsync();
            await CalculateSummary();
        }

        private async Task CalculateSummary()
        {
            await Task.Run(() =>
            {
                try
                {
                    _summary.Clear();
                    uint errorNum = 0;
                    uint totalActions = 0;
                    uint totalAttacks = 0;
                    uint totalKills = 0;
                    double killPercentage = 0;
                    foreach (var item in _toDoItems)
                    {
                        errorNum += item.ActionsError;
                        totalActions += item.ActionsTotal;
                        totalAttacks += item.AttackTotal;
                        totalKills += item.AttackKill;
                    }

                    killPercentage = Math.Round((double)totalKills / (double)totalAttacks * 100, 2);
                    _summary.Add(new Summary
                    {
                        Title = "Fehler",
                        Text = errorNum.ToString(),
                    });
                    _summary.Add(new Summary
                    {
                        Title = "Anzahl Aktionen Gesamt",
                        Text = totalActions.ToString(),
                    });
                    _summary.Add(new Summary
                    {
                        Title = "Anzahl Angriffe",
                        Text = totalAttacks.ToString(),
                    });
                    _summary.Add(new Summary
                    {
                        Title = "Angriffsquote",
                        Text = $"{killPercentage.ToString()}%",
                    });
                    StateHasChanged();
                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // throw;
                }
            });
        }

        private async Task LoadTodosFromServerAsync()
        {
            try
            {
                _toDoItems = await ToDoService.GetToDoListAsync();
                StateHasChanged();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load todo list.");
                _toDoItems = new List<ToDoData>();
            }
        }

        // events
        private async Task StartedEditingItem(ToDoItem item)
        {
            _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
            await RefreshAsync();
        }

        private async Task CanceledEditingItem(ToDoItem item)
        {
            _events.Insert(0, $"Event = CanceledEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
            await RefreshAsync();
        }

        private async Task CommittedItemChanges(ToDoItem item)
        {
            _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
            await RefreshAsync();
        }

        private async Task StartEditingItemAsync()
        {

        }

        /*private string RowStyleFunc(ToDoItem arg1, int index)
        {
            switch (arg1.Item.)
            {
                case string a when a.Contains("1/4"):
                    return "background-color:blue";
                case string b when b.Contains("2/4"):
                    return "background-color:red";
                default: return "background-color:white";

            }
        }*/
    }
}