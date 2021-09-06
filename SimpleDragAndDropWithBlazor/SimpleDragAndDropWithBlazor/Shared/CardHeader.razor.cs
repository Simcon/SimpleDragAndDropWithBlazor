using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class CardHeader : ComponentBase
    {
        [CascadingParameter] public CardModel CardModel { get; set; }
    }
}
