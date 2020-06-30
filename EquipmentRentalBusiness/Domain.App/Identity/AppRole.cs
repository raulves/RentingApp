using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId 
    {
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string DisplayName { get; set; } = default!;
        
    }
}