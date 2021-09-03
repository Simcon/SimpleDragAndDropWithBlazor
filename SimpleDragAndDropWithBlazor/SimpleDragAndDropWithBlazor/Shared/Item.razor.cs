using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class Item : ComponentBase
    {
        [CascadingParameter] BoardContainer Container { get; set; }
        [Parameter] public CardModel CardModel { get; set; }
        [Parameter] public ItemModel ItemModel { get; set; }

        public async Task OnUp()
        {
            var list = CardModel.Items;
            var oldIndex = list.FindIndex(i => i.Id == ItemModel.Id);
            if (oldIndex == 0) return;
            list.RemoveAt(oldIndex);
            list.Insert(oldIndex - 1, ItemModel);
            await Container.UpdateItemAsync();
        }

        public async Task OnDown()
        {
            var list = CardModel.Items;
            var oldIndex = list.FindIndex(i => i.Id == ItemModel.Id);
            if (oldIndex == list.Count - 1) return;
            list.RemoveAt(oldIndex);
            list.Insert(oldIndex + 1, ItemModel);
            await Container.UpdateItemAsync();
        }
    }
}

