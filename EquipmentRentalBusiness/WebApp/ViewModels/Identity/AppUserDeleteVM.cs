using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

#pragma warning disable 1591
namespace WebApp.ViewModels.Identity
{
    public class AppUserDeleteVM : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string FirstName { get; set; } = default!;
            
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string LastName { get; set; } = default!;
            
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string Email { get; set; } = default!;
    }
}