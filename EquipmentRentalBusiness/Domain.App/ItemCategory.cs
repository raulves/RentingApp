using System;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class  ItemCategory : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
    
}