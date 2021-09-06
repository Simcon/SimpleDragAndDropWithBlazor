using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class CardLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int Id { get; set; }
    }
}
