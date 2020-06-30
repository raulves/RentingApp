using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ImageDTOs
{
    public class ImageDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [MaxLength(4096)]
        public string? Picture { get; set; }

       
        public Guid ItemId { get; set; }
    }
}