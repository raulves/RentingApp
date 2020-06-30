using System;
using System.Diagnostics.CodeAnalysis;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PaymentBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentTypeBLL? PaymentType { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        public InvoiceBLL? Invoice { get; set; }
        
        public Guid? CompanyId { get; set; }
        public CompanyBLL? Company { get; set; }
    }
    
}