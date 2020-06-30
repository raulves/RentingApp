using System;
using System.Diagnostics.CodeAnalysis;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemOwnershipBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public Guid? CompanyId { get; set; }
        public CompanyBLL? Company { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemBLL? Item { get; set; }
    }
    
}