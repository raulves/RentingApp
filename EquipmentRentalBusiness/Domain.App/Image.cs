using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Image : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public byte[]? Picture { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
    }
    
}