using System;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemCategoryBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public CategoryBLL? Category { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemBLL? Item { get; set; }
    }
    
}