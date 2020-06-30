#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Identity;

namespace WebApp.ViewModels
{
    public class PriceCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUserViewModel? AppUser { get; set; }
        
        [Display(Name = nameof(ItemPrice), ResourceType = typeof(Resources.Domain.Price.Price))]
        public decimal ItemPrice { get; set; }
        
        [Display(Name = nameof(ItemId), ResourceType = typeof(Resources.Domain.Price.Price))]
        public Guid ItemId { get; set; }
        public ItemCreateEditViewModel? Item { get; set; }

        [Display(Name = nameof(RentalPeriodId), ResourceType = typeof(Resources.Domain.Price.Price))]
        public Guid RentalPeriodId { get; set; }
        public RentalPeriodCreateEditViewModel? RentalPeriod { get; set; }
        
    }
}