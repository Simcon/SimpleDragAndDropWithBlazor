using System;

namespace SimpleDragAndDropWithBlazor.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public CardStatuses Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public enum CardStatuses
    {
        Todo,
        Started,
        Completed
    }
}
