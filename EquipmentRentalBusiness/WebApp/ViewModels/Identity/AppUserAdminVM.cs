#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels.Identity
{
    public class AppUserAdminVM : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string Email { get; set; } = default!;
        
        public string UserName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; } = default!;
        
        
        [MaxLength(64)]
        [MinLength(1)]
        [Display(Name = nameof(Phone), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string? Phone { get; set; }
        
        [Display(Name = nameof(FullName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string FullName => FirstName + " " + LastName;

        [Display(Name = nameof(Revenue), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public decimal Revenue { get; set; }

        public LocationCreateEditViewModel? Location { get; set; }
        
        public ICollection<ItemCreateEditViewModel>? Items { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? BookingsAsItemOwner { get; set; }
        
        
        public ICollection<BookingsCreateEditViewModel>? BookingsAsRenter { get; set; }
    }
}