using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class CardHeaderLayout : ComponentBase
    {
        [Parameter] public RenderFragment DescriptionContent { get; set; }
        [Parameter] public RenderFragment DropDownContent { get; set; }
    }
}
