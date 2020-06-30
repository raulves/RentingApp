using System;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Price : DomainEntityIdMetadataUser<AppUser>
    {
        public decimal ItemPrice { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
        public Guid RentalPeriodId { get; set; } = default!;
        public RentalPeriod? RentalPeriod { get; set; }
    }
    
}