using System;
using System.Collections.Generic;

namespace SimpleDragAndDropWithBlazor.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    }

    //public enum CardStatuses
    //{
    //    Todo,
    //    Started,
    //    Completed
    //}

    public class ItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
