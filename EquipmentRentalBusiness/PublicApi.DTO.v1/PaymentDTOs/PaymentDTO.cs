using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid InvoiceId { get; set; }

        public Guid AppUserId { get; set; }

        public Guid? CompanyId { get; set; }
    }
}