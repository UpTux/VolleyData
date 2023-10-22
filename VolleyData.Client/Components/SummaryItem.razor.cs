using Microsoft.AspNetCore.Components;
using VolleyData.Client.Services;
using VolleyData.Shared.DTOs;

namespace VolleyData.Client.Components
{
    public partial class SummaryItem
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;

        [Parameter] public Summary Item { get; set; } = default!;

        [Parameter] public EventCallback<bool> SummaryItemChanged { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
