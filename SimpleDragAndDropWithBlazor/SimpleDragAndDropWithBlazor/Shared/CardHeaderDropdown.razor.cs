using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeaderDropdown : ComponentBase
    {
        [CascadingParameter] public BoardContainer Container { get; set; }
        [CascadingParameter] public CardModel CardModel { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        public string test { get; set; }

        private MessageUpdateInvokeHelper messageUpdateInvokeHelper;

        protected override void OnInitialized()
        {
            messageUpdateInvokeHelper = new MessageUpdateInvokeHelper(UpdateMessage);
            base.OnInitialized();
        }

        public void UpdateMessage()
        {
        }

        public async Task OnClick()
        {
            await JS.InvokeVoidAsync("updateMessageCaller",
                DotNetObjectReference.Create(messageUpdateInvokeHelper));
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
