using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class ProductDescription : DomainEntityIdMetadata
    {
        public Guid DescriptionId { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        public LangStr? Description { get; set; } = default!;

        public ICollection<ItemDescription>? ItemDescriptions { get; set; }
    }
    
}