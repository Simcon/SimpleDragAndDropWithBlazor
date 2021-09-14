using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class ItemLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
