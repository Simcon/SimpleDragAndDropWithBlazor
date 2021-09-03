using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;
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

        public CardModel CardPayload { get; set; }

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
            StateHasChanged();
        }
    }
}
