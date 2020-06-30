using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ProductDescriptionBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;

        public ICollection<ItemDescriptionBLL>? ItemDescriptions { get; set; }
    }
    
}