using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class RentalPeriod : DomainEntityIdMetadata
    {
        
        [MinLength(1)]
        [MaxLength(64)]
        public string Description { get; set; } = default!;
        
        public int PeriodStart { get; set; }
        
        public int PeriodEnd { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
    
}