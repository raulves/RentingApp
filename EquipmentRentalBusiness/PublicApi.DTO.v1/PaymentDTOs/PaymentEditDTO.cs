using System;

namespace PublicApi.DTO.v1.PaymentDTOs
{
    public class PaymentEditDTO
    {
        public Guid Id { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid InvoiceId { get; set; }

        public Guid? AppUserId { get; set; }

        public Guid? CompanyId { get; set; }
    }
}