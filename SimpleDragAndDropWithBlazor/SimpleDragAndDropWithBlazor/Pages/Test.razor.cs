﻿using Microsoft.AspNetCore.Components;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.Collections.Generic;

namespace SimpleDragAndDropWithBlazor.Pages
{
    public partial class Test : ComponentBase
    {
        List<CardModel> Cards = new List<CardModel>();
        public List<string> Output { get; set; } = new List<string>();

        protected override void OnInitialized()
        {
            Cards.Add(new CardModel
            {
                Id = 1,
                Description = "Mow the lawn",
                Status = CardStatuses.Todo,
                LastUpdated = DateTime.Now,
                Items = new List<ItemModel>
            {
                new ItemModel { Id = 1, Description = "First item" },
                new ItemModel { Id = 2, Description = "Second item" },
                new ItemModel { Id = 3, Description = "Third item" }
            }
            });
            Cards.Add(new CardModel
            {
                Id = 2,
                Description = "Go to the gym",
                Status = CardStatuses.Todo,
                LastUpdated = DateTime.Now,
                Items = new List<ItemModel>
            {
                new ItemModel { Id = 4, Description = "A item" },
                new ItemModel { Id = 5, Description = "B item" },
                new ItemModel { Id = 6, Description = "C item" }
            }
            });
            Cards.Add(new CardModel { Id = 3, Description = "Call Ollie", Status = CardStatuses.Todo, LastUpdated = DateTime.Now });
            Cards.Add(new CardModel { Id = 4, Description = "Fix bike tyre", Status = CardStatuses.Todo, LastUpdated = DateTime.Now });
            Cards.Add(new CardModel { Id = 5, Description = "Finish blog post", Status = CardStatuses.Todo, LastUpdated = DateTime.Now });
        }

        void HandleStatusUpdated(CardModel updatedJob)
        {
            //lastUpdatedJob = updatedJob.Description;
            Output.Add(updatedJob.Description);
        }
    }
}
