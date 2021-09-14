using Microsoft.AspNetCore.Components;

namespace SimpleDragAndDropWithBlazor.Shared.Layouts
{
    public partial class CardLayout : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int Id { get; set; }
        [Parameter] public bool IsDropdownVisible { get; set; }

        private string CssClass => IsDropdownVisible ? "muuri-card absolute z-50" : "muuri-card absolute z-0";
    }
}
