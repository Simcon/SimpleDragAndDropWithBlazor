using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeaderDropdown : ComponentBase
    {
        [CascadingParameter] public CardModel CardModel { get; set; }
    }
}
