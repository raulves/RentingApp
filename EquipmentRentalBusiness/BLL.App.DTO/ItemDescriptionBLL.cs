using System;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemDescriptionBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public Guid ProductDescriptionId { get; set; } = default!;
        public ProductDescriptionBLL? ProductDescription { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemBLL? Item { get; set; }
    }
    
}