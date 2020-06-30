using System;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ItemDescriptionDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public Guid ProductDescriptionId { get; set; } = default!;
        [JsonIgnore]
        public ProductDescriptionDAL? ProductDescription { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        [JsonIgnore]
        public ItemDAL? Item { get; set; }
    }
    
}