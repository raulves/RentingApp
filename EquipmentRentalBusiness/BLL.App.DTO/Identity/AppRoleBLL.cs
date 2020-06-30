using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppRoleBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string DisplayName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string Name { get; set; } = default!;
    }
}