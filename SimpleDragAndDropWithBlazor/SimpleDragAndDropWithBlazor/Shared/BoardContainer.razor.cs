using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class BoardContainer : ComponentBase
    {
        [Parameter] public List<CardModel> Cards { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<CardModel> OnStatusUpdated { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] ProtectedSessionStorage ProtectedSessionStore { get; set; }

        public CardModel CardPayload { get; set; }
        private LayoutUpdateInvokeHelper layoutUpdateInvokeHelper;

        protected override async Task OnInitializedAsync()
        {
            var pop = await ProtectedSessionStore.GetAsync<LayoutChange[][]>("layout");

            layoutUpdateInvokeHelper = new LayoutUpdateInvokeHelper(LayoutUpdate);
            base.OnInitialized();

            var objref = DotNetObjectReference.Create(layoutUpdateInvokeHelper);
            await JS.InvokeVoidAsync("setref", objref);
            await base.OnInitializedAsync();
        }

        private async Task LayoutUpdate(LayoutChange[][] cards)
        {
            await ProtectedSessionStore.SetAsync("layout", cards);
        }

        public async Task UpdateCardAsync()
        {
            var job = Cards.SingleOrDefault(x => x.Id == CardPayload.Id);

            if (job != null)
            {
                await OnStatusUpdated.InvokeAsync(CardPayload);
            }
        }

        public async Task UpdateItemAsync()
        {
            await InvokeAsync(() => StateHasChanged());
        }
    }

    public class LayoutUpdateInvokeHelper
    {
        private Func<LayoutChange[][], Task> action;

        public LayoutUpdateInvokeHelper(Func<LayoutChange[][], Task> action)
        {
            this.action = action;
        }

        [JSInvokable("UpdateLayoutCaller")]
        public void UpdateLayoutCaller(LayoutChange[][] cards)
        {
            action.Invoke(cards);
        }
    }

    public class LayoutChange
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
