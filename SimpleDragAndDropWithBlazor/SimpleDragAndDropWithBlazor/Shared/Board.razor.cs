using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class Board : ComponentBase
    {
        [CascadingParameter] BoardContainer Container { get; set; }
        [Parameter] public string CardStatus { get; set; }
        List<CardModel> Cards = new List<CardModel>();

        protected override void OnParametersSet()
        {
            Cards.Clear();
            Cards.AddRange(Container.Cards.Where(x => x.Status == CardStatus));
        }
    }
}
