using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeaderButton : ComponentBase
    {
        [CascadingParameter] public BoardContainer Container { get; set; }
        [CascadingParameter] public CardModel CardModel { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        private MessageUpdateInvokeHelper _messageUpdateInvokeHelper;

        protected override void OnInitialized()
        {
            _messageUpdateInvokeHelper = new MessageUpdateInvokeHelper(UpdateMessage);
            base.OnInitialized();
        }

        public void UpdateMessage()
        {
            //Container.some_method
        }

        public async Task OnClick()
        {
            CardModel.IsDropdownVisible = !CardModel.IsDropdownVisible;
            await ApplyZIndex();
            StateHasChanged();
        }

        public async Task OnFocusOut()
        {
            CardModel.IsDropdownVisible = false;
            await ApplyZIndex();
        }

        private async Task ApplyZIndex()
        {
            var zIndex = CardModel.IsDropdownVisible ? "11" : "10";
            await JS.InvokeAsync<object>("applyStyleForElement",
                new { id = $"cardlayout_{CardModel.Id}", attrib = "z-index", value = zIndex });
        }
    }

    public class MessageUpdateInvokeHelper
    {
        private Action action;

        public MessageUpdateInvokeHelper(Action action)
        {
            this.action = action;
        }

        [JSInvokable("UpdateMessageCaller")]
        public void UpdateMessageCaller()
        {
            action.Invoke();
        }
    }
}
