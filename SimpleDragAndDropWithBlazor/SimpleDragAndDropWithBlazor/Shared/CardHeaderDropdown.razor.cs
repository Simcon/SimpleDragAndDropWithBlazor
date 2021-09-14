using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeaderDropdown : ComponentBase
    {
        [CascadingParameter] public CardModel CardModel { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        private void Button1Click()
        {
            Nav.NavigateTo("/test2");
        }

        private void Button2Click()
        {
            Nav.NavigateTo("/test2");
        }

        private void Button3Click()
        {
            Nav.NavigateTo("/test2");
        }
    }
}
