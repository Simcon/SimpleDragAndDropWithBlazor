using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class BoardContainerLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
