#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels
{
    public class ItemViewProfileVM : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Description { get; set; } = default!;
        
        [Display(Name = nameof(Brand), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Brand { get; set; } = default!;
        
        [Display(Name = nameof(Model), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Model { get; set; } = default!;
        
        [Display(Name = nameof(Location), ResourceType = typeof(Resources.Domain.Item.Item))]
        public LocationCreateEditViewModel? Location { get; set; }
    }
}