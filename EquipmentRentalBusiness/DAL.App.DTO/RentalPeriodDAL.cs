using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace DAL.App.DTO
{
    public class RentalPeriodDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; } = default!;
        
        public int PeriodStart { get; set; }
        
        public int PeriodEnd { get; set; }
        

        [JsonIgnore]
        public ICollection<PriceDAL>? Prices { get; set; }
    }
    
}