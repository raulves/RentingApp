using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ItemView : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; } = default!;

        // Kuvan ainult ühe pildi
        [MaxLength(4096)]
        [MinLength(1)]
        public byte[]? Image { get; set; }
        public string AddressLine { get; set; } = default!;
        
        public decimal Price { get; set; }
    }
}