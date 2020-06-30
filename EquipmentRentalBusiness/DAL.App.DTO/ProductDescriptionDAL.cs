using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ProductDescriptionDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;

        [JsonIgnore]
        public ICollection<ItemDescriptionDAL>? ItemDescriptions { get; set; }
    }
    
}