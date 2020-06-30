using System;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class ItemOwnership : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
    
}