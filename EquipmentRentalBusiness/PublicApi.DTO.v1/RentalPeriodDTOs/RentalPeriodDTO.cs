using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.RentalPeriodDTOs
{
    public class RentalPeriodDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string Description { get; set; } = default!;
        public int PeriodStart { get; set; }
        public int PeriodEnd { get; set; }
    }
}