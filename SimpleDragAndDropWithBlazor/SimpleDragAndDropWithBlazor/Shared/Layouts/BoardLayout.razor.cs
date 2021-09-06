using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class BoardLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
