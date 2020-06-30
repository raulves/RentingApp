using System;
using BLL.App.DTO.Identity;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PriceBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public decimal ItemPrice { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemBLL? Item { get; set; }
        
        public Guid RentalPeriodId { get; set; } = default!;
        public RentalPeriodBLL? RentalPeriod { get; set; }
    }
    
}