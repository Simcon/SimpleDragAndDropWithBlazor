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

        //public bool IsDropdownVisible { get; set; }
        private MessageUpdateInvokeHelper messageUpdateInvokeHelper;

        protected override void OnInitialized()
        {
            messageUpdateInvokeHelper = new MessageUpdateInvokeHelper(UpdateMessage);
            base.OnInitialized();
        }

        public void UpdateMessage()
        {
            //Container.up
        }

        public async Task OnClick()
        {
            CardModel.IsDropdownVisible = !CardModel.IsDropdownVisible;
            await ShowHideDropdown();
        }

        public async Task OnFocusOut()
        {
            CardModel.IsDropdownVisible = false;
            await ShowHideDropdown();
        }

        private async Task ShowHideDropdown()
        {
            if (CardModel.IsDropdownVisible)
            {
                await JS.InvokeAsync<object>("applyStyleForElement",
                    new { id = $"cardlayout_{CardModel.Id}", attrib = "z-index", value = "11" });
            }
            else
            {
                await JS.InvokeAsync<object>("applyStyleForElement",
                    new { id = $"cardlayout_{CardModel.Id}", attrib = "z-index", value = "10" });
            }
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
