using Grpc.Core;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProtoBuf.Grpc;
using VolleyData.Client.Services;
using VolleyData.Shared.Services;

namespace VolleyData.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject] private ITimeService TimeService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        [Inject] private IToDoService ToDoService { get; set; } = default!;
        [Inject] private ToDoService tdService { get; set; } = default!;

        private string _time = "";
        private bool _canUndo = true;
        private bool _isClearAllEnabled = false;
        private bool _isResetEnabled = false;
        private CancellationTokenSource _cts;

        private MudTheme _currentTheme = new();

        private readonly MudTheme _defaultTheme = new()
        {
            //Palette = new Palette()
            //{
            //    Black = "#272c34",
            //    AppbarBackground = "#ffffff",
            //    AppbarText = "#ff584f",
            //    ActionDefault = "#ff584f",
            //    Primary = "#ff584f",
            //    Secondary = "#3d6fb4"
            //}
        };

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = _defaultTheme;
            await StartTime();
            await base.OnInitializedAsync();
        }

        public void Dispose()
        {
            StopTime();
            _canUndo = false;
        }

        private async Task StartTime()
        {
            _cts = new CancellationTokenSource();
            var options = new CallOptions(cancellationToken: _cts.Token);

            try
            {
                await foreach (var time in TimeService.SubscribeAsync(new CallContext(options)))
                {
                    _time = time;
                    StateHasChanged();
                }
            }
            catch (RpcException)
            {
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void StopTime()
        {
            _cts?.Cancel();
            _cts = null;
            _time = "";
        }

        private async Task UndoAsync()
        {
            await tdService.UndoAsync();
            StateHasChanged();

            //NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            //StateHasChanged();
        }

        private async Task ClearAllAsync()
        {
            await ToDoService.ClearAllAsync();
            StateHasChanged();
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            StateHasChanged();
        }

        private async Task ResetAllAsync()
        {
            await ToDoService.ResetAllAsync();
            StateHasChanged();
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            StateHasChanged();
        }
    }
}