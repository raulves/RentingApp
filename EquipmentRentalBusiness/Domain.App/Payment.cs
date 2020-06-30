using System;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Payment : DomainEntityIdMetadataUser<AppUser>
    {
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
    
}