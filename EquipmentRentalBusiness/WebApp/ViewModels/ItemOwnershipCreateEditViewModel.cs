#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Identity;

namespace WebApp.ViewModels
{
    public class ItemOwnershipCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(AppUserId), ResourceType = typeof(Resources.Domain.ItemOwnership.ItemOwnership))]
        public Guid AppUserId { get; set; }
        public AppUserViewModel? AppUser { get; set; }

        [Display(Name = nameof(CompanyId), ResourceType = typeof(Resources.Domain.ItemOwnership.ItemOwnership))]
        public Guid? CompanyId { get; set; }
        public CompanyCreateEditViewModel? Company { get; set; }

        [Display(Name = nameof(ItemId), ResourceType = typeof(Resources.Domain.ItemOwnership.ItemOwnership))]
        public Guid ItemId { get; set; }
        public ItemCreateEditViewModel? Item { get; set; }

        public SelectList? AppUserSelectList { get; set; }
        public SelectList? CompanySelectList { get; set; }
        public SelectList? ItemSelectList { get; set; }
        
    }
}