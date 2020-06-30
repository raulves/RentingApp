using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class ItemDescription : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string Description { get; set; } = default!;
        
        public Guid ProductDescriptionId { get; set; } = default!;
        public ProductDescription? ProductDescription { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
    
}