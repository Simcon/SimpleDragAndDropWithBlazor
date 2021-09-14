using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class BoardContainer : ComponentBase
    {
        /*[Parameter]*/
        public List<CardModel> Cards { get; set; } = new List<CardModel>();
        public List<DbBoard> Boards { get; set; } = new List<DbBoard>();

        //[Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<CardModel> OnStatusUpdated { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        //[Inject] ProtectedSessionStorage ProtectedSessionStore { get; set; }

        public CardModel CardPayload { get; set; }
        private LayoutUpdateInvokeHelper layoutUpdateInvokeHelper;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //if (firstRender)
            //{
            //    await JS.InvokeVoidAsync("initMuuri");
            //}
            //await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            var db = await LoadFromDb();
            Cards = db.Item1;
            Boards = db.Item2;
            StateHasChanged();

            layoutUpdateInvokeHelper = new LayoutUpdateInvokeHelper(LayoutUpdate);
            base.OnInitialized();

            var objref = DotNetObjectReference.Create(layoutUpdateInvokeHelper);
            await JS.InvokeVoidAsync("setref", objref);
            await JS.InvokeVoidAsync("initMuuri");
            //await JS.InvokeVoidAsync("testEventListeners");
        }

        private async Task LayoutUpdate(LayoutChange[][] cards)
        {
            //await SaveToDb(cards);
        }

        //public async Task UpdateCardAsync() // is this redundant?
        //{
        //    var job = Cards.SingleOrDefault(x => x.Id == CardPayload.Id);

        //    if (job != null)
        //    {
        //        await OnStatusUpdated.InvokeAsync(CardPayload);
        //    }
        //}

        //public async Task UpdateItemAsync()
        //{
        //    await InvokeAsync(() => StateHasChanged());
        //}

        private async Task<(List<CardModel>, List<DbBoard>)> LoadFromDb()
        {
            //var pop = await ProtectedSessionStore.GetAsync<DbBoardLayout>("data");
            //return pop.Success ? DbBoardLayout.MapFrom(pop.Value) : DbBoardLayout.MapFrom(DbBoardLayout.DefaultDbData);
            return DbBoardLayout.MapFrom(DbBoardLayout.DefaultDbData);
        }

        //private async Task SaveToDb(LayoutChange[][] cards)
        //{
        //    var data = DbBoardLayout.MapTo(cards);
        //    await ProtectedSessionStore.SetAsync("data", data);
        //}
    }

    public class LayoutUpdateInvokeHelper
    {
        private Func<LayoutChange[][], Task> action;

        public LayoutUpdateInvokeHelper(Func<LayoutChange[][], Task> action)
        {
            this.action = action;
        }

        [JSInvokable("UpdateLayoutCaller")]
        public void UpdateLayoutCaller(LayoutChange[][] cards)
        {
            action.Invoke(cards);
        }
    }

    public class LayoutChange
    {
        public string Id { get; set; }
    }

    public class DbBoardLayout
    {
        public List<DbBoard> Boards = new List<DbBoard>();
        public List<DbCard> Cards = new List<DbCard>();

        public static (List<CardModel>, List<DbBoard>) MapFrom(DbBoardLayout data)
        {
            var cards = data.Cards.Select(c => new CardModel
            {
                Id = c.Id,
                Description = c.Title,
                Status = c.BoardId.ToString(),
                Items = c.Items.Select(i => new ItemModel { Id = i.Id, Description = i.Description }).ToList()
            }).ToList();
            return (cards, data.Boards);
        }

        public static DbBoardLayout MapTo(LayoutChange[][] layout)
        {
            var result = new DbBoardLayout();

            result.Boards = DefaultDbData.Boards;

            for (var i = 0; i < layout.Length; i++)
            {
                foreach (var card in layout[i])
                {
                    var dbCard = DefaultDbData.Cards.SingleOrDefault(c => c.Id.ToString() == card.Id);

                    if (dbCard == null) continue;

                    result.Cards.Add(new DbCard
                    {
                        Id = dbCard.Id,
                        Title = dbCard.Title,
                        BoardId = DefaultDbData.Boards[i].Title,
                        Items = dbCard.Items,
                        LastUpdated = dbCard.LastUpdated
                    });
                }
            }

            return result;
        }

        public static DbBoardLayout DefaultDbData =>
          new DbBoardLayout
          {
              Boards = new List<DbBoard>
              {
                    new DbBoard { Id = 1, Title = "Board One" },
                    new DbBoard { Id = 2, Title = "Board Two" },
                    new DbBoard {Id = 3, Title = "Board Three" }
              },
              Cards = new List<DbCard>
                        {
                            new DbCard
                            {
                                Id = 1, BoardId = "Board One", Title = "Mow the lawn", Items = new List<DbItem>
                                {
                                    new DbItem { Id = 1, Description = "First item" },
                                    new DbItem { Id = 2, Description = "Second item" },
                                    new DbItem { Id = 3, Description = "Third item" }
                                }
                            },
                            new DbCard { Id = 3, BoardId = "Board One", Title = "Call Ollie" },
                            new DbCard { Id = 4, BoardId = "Board One", Title = "Fix bike tyre" },
                            new DbCard { Id = 5, BoardId = "Board One", Title = "Finish blog post" },
                            new DbCard
                            {
                                Id = 2, BoardId = "Board One", Title = "Go to the gym", Items = new List<DbItem>
                                {
                                    new DbItem { Id = 4, Description = "A item" },
                                    new DbItem { Id = 5, Description = "B item" },
                                    new DbItem { Id = 6, Description = "C item" }
                                }
                            },
                        }
          };
    }

    public class DbBoard
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class DbCard
    {
        public int Id { get; set; }
        public string BoardId { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<DbItem> Items { get; set; } = new List<DbItem>();
    }

    public class DbItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
