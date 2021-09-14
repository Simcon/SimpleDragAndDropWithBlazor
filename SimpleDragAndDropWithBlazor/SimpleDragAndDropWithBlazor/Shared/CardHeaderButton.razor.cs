using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeaderButton : ComponentBase
    {
        [CascadingParameter] public CardModel CardModel { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

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
}
