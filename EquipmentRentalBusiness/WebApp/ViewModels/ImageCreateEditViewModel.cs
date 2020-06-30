#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ImageCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [MaxLength(4096)]
        [Display(Name = nameof(Picture), ResourceType = typeof(Resources.Domain.Image.Image))]
        public byte[]? Picture { get; set; }
        
        [Display(Name = nameof(ItemId), ResourceType = typeof(Resources.Domain.Image.Image))]
        public Guid ItemId { get; set; }
        public ItemCreateEditViewModel? Item { get; set; }

        public SelectList? ItemSelectList { get; set; }
        
        [Display(Name = nameof(Images), ResourceType = typeof(Resources.Domain.Image.Image))]
        public ICollection<ImageCreateEditViewModel>? Images { get; set; }
    }
}