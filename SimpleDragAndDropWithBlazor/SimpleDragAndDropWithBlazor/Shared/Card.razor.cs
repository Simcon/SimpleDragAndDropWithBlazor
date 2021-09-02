using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class Card : ComponentBase
    {
        [CascadingParameter] BoardContainer Container { get; set; }
        [Parameter] public CardModel CardModel { get; set; }

        public async Task OnStart()
        {
            //CardModel.Status = CardStatuses.Completed;
            //await OnStatusUpdated.InvokeAsync(Payload);
            //await Container.OnStatusUpdated.InvokeAsync(CardModel);

            // set the payload
            CardModel.Status = CardStatuses.Started;
            Container.Payload = CardModel;

            // for drag and drop this would be called from the boardlist ondrop event,
            // but for now we'll just do it here
            await Container.UpdateCardAsync();
        }
        public async Task OnComplete()
        {
            CardModel.Status = CardStatuses.Completed;
            Container.Payload = CardModel;
            await Container.UpdateCardAsync();
        }
    }
}
