#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemDescriptionCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.ItemDescription.ItemDescription))]
        public string Description { get; set; } = default!;
        public Guid AppUserId { get; set; }
        
        [Display(Name = nameof(ProductDescriptionId), ResourceType = typeof(Resources.Domain.ItemDescription.ItemDescription))]
        public Guid ProductDescriptionId { get; set; }
        public ProductDescriptionCreateEditViewModel? ProductDescription { get; set; }

        [Display(Name = nameof(ItemId), ResourceType = typeof(Resources.Domain.ItemDescription.ItemDescription))]
        public Guid ItemId { get; set; }
        public ItemCreateEditViewModel? Item { get; set; }
        public SelectList? ItemSelectList { get; set; }
        public SelectList? ProductDescriptionSelectList { get; set; }
        
    }
}