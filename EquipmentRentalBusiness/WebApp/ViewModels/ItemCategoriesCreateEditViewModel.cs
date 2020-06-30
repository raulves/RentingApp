#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemCategoriesCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [Display(Name = nameof(CategoryId), ResourceType = typeof(Resources.Domain.ItemCategory.ItemCategory))]
        public Guid CategoryId { get; set; }
        public CategoryCreateEditViewModel? Category { get; set; }

        [Display(Name = nameof(ItemId), ResourceType = typeof(Resources.Domain.ItemCategory.ItemCategory))]
        public Guid ItemId { get; set; }
        public ItemCreateEditViewModel? Item { get; set; }

        public SelectList? CategorySelectList { get; set; }
        public SelectList? ItemSelectList { get; set; }
        
    }
}