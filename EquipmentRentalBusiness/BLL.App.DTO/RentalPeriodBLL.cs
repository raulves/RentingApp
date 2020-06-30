using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class RentalPeriodBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public int PeriodStart { get; set; }
        
        public int PeriodEnd { get; set; }
        

        public ICollection<PriceBLL>? Prices { get; set; }
    }
    
}