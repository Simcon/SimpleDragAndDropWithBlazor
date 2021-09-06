using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class SectionLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
